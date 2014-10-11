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
        [Test]
        public void Test()
        {
            var room = new PlanningRoom
            {
                Users = new List<string> { "Andrei", "Ulad" },
                Sessions = new List<VotingSession>
                {
                    new VotingSession
                    {
                        Description = "Do this",
                        VotedAt = DateTime.UtcNow,
                        Votes = new List<Vote>{new Vote("Andrei", 5), new Vote("Ulad", 3)}
                    },
                    new VotingSession
                    {
                        Description = "Do that",
                        VotedAt = DateTime.UtcNow,
                        Votes = new List<Vote>{new Vote("Andrei", 1), new Vote("Ulad", 8)},
                        AgreedVote = 5
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
        public void Qwa()
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