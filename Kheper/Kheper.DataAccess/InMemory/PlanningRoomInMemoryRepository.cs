using Kheper.Core.Model;
using Kheper.Core.Store;

namespace Kheper.DataAccess.InMemory
{
    using System;
    using System.Collections.Generic;

    using Kheper.Core.Dependency;

    [Singleton]
    [Component(Precedence = EPrecedence.Development)]
    public class PlanningRoomInMemoryRepository : AbstractInMemoryRepository<PlanningRoom, long>, IPlanningRoomRepository
    {
        private long lastId;

        public PlanningRoomInMemoryRepository()
        {
            Populate();
        }

        private void Populate()
        {
            var room = new PlanningRoom
            {
                Id = 1,
                Users = new List<string> { "Andrei", "Uladzimir" },
                Sessions = new Dictionary<long, VotingSession>
                {
                    {
                        1,
                        new VotingSession
                        {
                            Id = 1,
                            RoomId = 1,
                            Description = "Do this",
                            VotedAt = DateTime.UtcNow,
                            Votes = new Dictionary<long, Vote>
                            {
                                {1, new Vote {PlanningRoomId = 1, VotingSessionId = 1, Id = 1, UserName = "Andrei", Value = "5"}}, 
                                {2, new Vote {PlanningRoomId = 1, VotingSessionId = 1, Id = 2, UserName = "Uladzimir", Value ="3"}}
                            }
                        }
                    },
                    {
                        2,
                        new VotingSession
                        {
                            Id = 2,
                            RoomId = 1,
                            Description = "Do that",
                            VotedAt = DateTime.UtcNow,
                            Votes = new Dictionary<long, Vote>
                            {
                                {3, new Vote {PlanningRoomId = 1, VotingSessionId = 2, Id = 3, UserName = "Andrei", Value = "5"}}, 
                                {4, new Vote {PlanningRoomId = 1, VotingSessionId = 2, Id = 4, UserName = "Uladzimir", Value ="3"}}
                            },
                            AgreedVote = "5"
                        }
                    }
                }
            };
            this.Save(room);
        }

        protected override long? GetId(PlanningRoom instance)
        {
            return instance.Id == 0 ? (long?)null : instance.Id;
        }

        protected override void SetId(PlanningRoom instance, long id)
        {
            instance.Id = id;
        }

        protected override long GenerateId(PlanningRoom instance)
        {
            return ++lastId;
        }
    }
}
