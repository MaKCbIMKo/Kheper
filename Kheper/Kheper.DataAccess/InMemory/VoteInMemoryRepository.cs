using System.Collections.Generic;
using System.Linq;

namespace Kheper.DataAccess.InMemory
{
    using Kheper.Core.Dependency;
    using Kheper.Core.Model;
    using Kheper.Core.Store;

    [Singleton]
    [Component(Precedence = EPrecedence.Development)]
    public class VoteInMemoryRepository : AbstractInMemoryRepository<Vote, VoteId>, IVoteRepository
    {
        private readonly IVotingSessionRepository _votingSessionRepository;

        private long lastId;

        public VoteInMemoryRepository(IVotingSessionRepository votingSessionRepository)
        {
            this._votingSessionRepository = votingSessionRepository;
        }

        protected override VoteId? GetId(Vote instance)
        {
            return new VoteId
            {
                PlanningRoomId = instance.PlanningRoomId,
                VotingSessionId = instance.VotingSessionId,
                Id = instance.Id
            };
        }

        protected override void SetId(Vote instance, VoteId id)
        {
            instance.PlanningRoomId = id.PlanningRoomId;
            instance.VotingSessionId = id.VotingSessionId;
            instance.Id = id.Id;
        }

        protected override VoteId GenerateId(Vote instance)
        {
            return new VoteId
            {
                VotingSessionId = instance.VotingSessionId,
                PlanningRoomId = instance.PlanningRoomId,
                Id = ++lastId
            };
        }

        public override Vote Query(VoteId id)
        {
            var votingSessionId = new VotingSessionId
            {
                PlanningRoomId = id.PlanningRoomId,
                Id = id.VotingSessionId
            };
            var session = this._votingSessionRepository.Query(votingSessionId);
            var vote = session.Votes.Values.SingleOrDefault(v => v.Id == id.Id);
            if (vote == null)
            {
                ThrowInstanceNotFound(id);
            }
            return vote;
        }

        public override IQueryable<Vote> Query()
        {
            var votes = new List<Vote>();
            var sessions = this._votingSessionRepository.Query();
            foreach (var session in sessions)
            {
                votes.AddRange(session.Votes.Values);
            }
            return votes.AsQueryable();
        }

        public override Vote Save(Vote instance)
        {
            instance = this.Clone(instance);
            var sessionId = new VotingSessionId
            {
                PlanningRoomId = instance.PlanningRoomId,
                Id = instance.VotingSessionId
            };
            var session = _votingSessionRepository.Query(sessionId);
            if (instance.Id == 0)
            {
                var id = this.GenerateId(instance);
                this.SetId(instance, id);
            }
            session.AddOrUpdateVote(instance);
            return instance;
        }

        public override void Delete(Vote instance)
        {
            var sessionId = new VotingSessionId
            {
                PlanningRoomId = instance.PlanningRoomId,
                Id = instance.VotingSessionId
            };
            var session = _votingSessionRepository.Query(sessionId);
            session.Votes.Remove(instance.Id);
            _votingSessionRepository.Save(session);
        }
    }
}
