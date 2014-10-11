using System.Web.Http;
using Kheper.Core.Model;
using Kheper.Core.Store;

namespace Kheper.Web.Controllers
{
    [RoutePrefix("api/planningrooms")]
    public class PlanningRoomController : ApiController
    {
        private readonly IPlanningRoomRepository _planningRoomRepository;

        private readonly IVotingSessionRepository _votingSessionRepository;

        private readonly IVoteRepository _voteRepository;

        public PlanningRoomController(IPlanningRoomRepository planningRoomRepository,
            IVotingSessionRepository votingSessionRepository,
            IVoteRepository voteRepository)
        {
            this._planningRoomRepository = planningRoomRepository;
            this._voteRepository = voteRepository;
            this._votingSessionRepository = votingSessionRepository;
        }

        [HttpGet]
        [Route("{roomId}")]
        public PlanningRoom Get(long roomId)
        {
            return this._planningRoomRepository.Query(roomId);
        }

        [HttpPost]
        [Route("{roomId}/join")]
        public PlanningRoom JoinExistingRoom(long roomId, JoinExistingRoomAction parameters)
        {
            var room = this._planningRoomRepository.Query(roomId);
            room.Users.Add(parameters.UserName);
            this._planningRoomRepository.Save(room);
            return room;
        }

        /// <summary>
        /// This WEB Method creates new planning room.
        /// </summary>
        /// <returns>Instance of <see cref="PlanningRoom"/></returns>
        [HttpPost]
        [Route("")]
        public PlanningRoom CreateNewRoom(CreateNewRoomAction parameters)
        {
            var room = new PlanningRoom();
            room.Users.Add(parameters.UserName);
            room = this._planningRoomRepository.Save(room);
            return room;
        }

        [HttpPost]
        [Route("{roomId}/votingsessions")]
        public VotingSession StartNewSession(long roomId, StartNewSessionAction parameters)
        {
            var session = new VotingSession { RoomId = roomId };
            session = this._votingSessionRepository.Save(session);
            return session;
        }

        [HttpPost]
        [Route("{roomId}/votingsessions/{sessionId}/votes")]
        public Vote CastVote(long roomId, long sessoinId, CastVoteAction parameters)
        {
            var vote = new Vote
            {
                PlanningRoomId = roomId,
                VotingSessionId = sessoinId,
                UserName = parameters.UserName,
                Value = parameters.Value
            };
            vote = _voteRepository.Save(vote);
            return vote;
        }

        #region DTO

        public class CreateNewRoomAction
        {
            public string UserName { get; set; }
        }

        public class JoinExistingRoomAction
        {
            public string UserName { get; set; }
        }

        public class StartNewSessionAction
        {
        }

        public class CastVoteAction
        {
            public string UserName { get; set; }
            public string Value { get; set; }
        }

        #endregion
    }
}

 