using System;
using System.Collections.Generic;
using NUnit.Framework;
using Raven.Client.Document;
using System.Linq;
using Kheper.Core.Model;
using Kheper.DataAccess.RavenDb;

namespace Kheper.Tests.Store
{
    public class RavenDbPlanningStoreTests
    {
        [Ignore("Manual run tests")]
        [Test]
        public void CanCreate()
        {
            var room = new PlanningRoom
            {
                Users = new List<string> { "Andrei", "Ulad" },
                Sessions = new Dictionary<long, VotingSession>
                {
                    {
                        1,
                        new VotingSession
                        {
                            Description = "Do this",
                            VotedAt = DateTime.UtcNow,
                            Votes = new Dictionary<long, Vote>
                            {
                                {1, new Vote {Id = 1, UserName = "Andrei", Value = "5"}}, 
                                {1, new Vote {Id=1, UserName = "Ulad", Value ="3"}}
                            }
                        }
                    },
                    {
                        1,
                        new VotingSession
                        {
                            Description = "Do that",
                            VotedAt = DateTime.UtcNow,
                            Votes = new Dictionary<long, Vote>
                            {
                                {1, new Vote {Id = 1, UserName = "Andrei", Value = "5"}}, 
                                {1, new Vote {Id=1, UserName = "Ulad", Value ="3"}}
                            },
                            AgreedVote = "5"
                        }
                    }
                }
            };

            var docStore = new DocumentStore
            {
                Url = "http://epbygomw0294:9123",
                DefaultDatabase = "kheper"
            };
            docStore.Initialize();

            using (var session = docStore.OpenSession())
            {
                session.Store(room);
                session.SaveChanges();
            }
        }

        [Test]
        public void CanDelete()
        {
            var docStore = new DocumentStore
            {
                Url = "http://epbygomw0294:9123",
                DefaultDatabase = "kheper"
            };
            docStore.Initialize();


            var repo = new PlanningRoomRavenRepository(docStore);
            var room = repo.Query().First();

            repo = new PlanningRoomRavenRepository(docStore);
            repo.Delete(room);
        }
    }
}