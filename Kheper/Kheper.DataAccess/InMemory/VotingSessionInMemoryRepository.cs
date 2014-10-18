namespace Kheper.DataAccess.InMemory
{
    using System.Collections.Generic;
    using System.Linq;

    using Kheper.Core.Dependency;
    using Kheper.Core.Model;
    using Kheper.Core.Store;

    [Autowire(typeof(IVotingSessionRepository), Precedence = EPrecedence.Development)]
    public class VotingSessionInMemoryRepository : AbstractInMemoryRepository<VotingSession, VotingSessionId>, IVotingSessionRepository
    {
        private long _lastId;

        private readonly IPlanningRoomRepository _roomRepository;

        public VotingSessionInMemoryRepository(IPlanningRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        protected override VotingSessionId? GetId(VotingSession instance)
        {
            return new VotingSessionId { PlanningRoomId = instance.RoomId, Id = instance.Id };
        }

        protected override void SetId(VotingSession instance, VotingSessionId id)
        {
            instance.RoomId = id.PlanningRoomId;
            instance.Id = id.Id;
        }

        protected override VotingSessionId GenerateId(VotingSession instance)
        {
            long sessionId = ++this._lastId;
            return new VotingSessionId { PlanningRoomId = instance.RoomId, Id = sessionId };
        }

        public override VotingSession Query(VotingSessionId id)
        {
            var room = _roomRepository.Query(id.PlanningRoomId);
            var votingSession = room.Sessions.Values.SingleOrDefault(session => session.Id == id.Id);
            if (votingSession == null)
            {
                ThrowInstanceNotFound(id);
            }
            return votingSession;
        }

        public override IQueryable<VotingSession> Query()
        {
            var sessions = new List<VotingSession>();
            var rooms = _roomRepository.Query();
            foreach (var planningRoom in rooms)
            {
                sessions.AddRange(planningRoom.Sessions.Values);
            }
            return sessions.AsQueryable();
        }

        public override VotingSession Save(VotingSession instance)
        {
            instance = this.Clone(instance);
            var room = _roomRepository.Query(instance.RoomId);
            if (instance.Id == 0)
            {
                var id = this.GenerateId(instance);
                this.SetId(instance, id);
            }
            room.AddOrUpdateSession(instance);
            return instance;
        }

        public override void Delete(VotingSession instance)
        {
            var room = _roomRepository.Query(instance.RoomId);
            room.Sessions.Remove(instance.Id);
            _roomRepository.Save(room);
        }
    }
}
