using System;
using System.Collections.Generic;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Document;
using System.Linq;
using Kheper.Core.Model;
using Kheper.DataAccess.RavenDb;
using Raven.Client.Embedded;

namespace Kheper.Tests.Store
{
    [TestFixture]
    public class RavenDbPlanningStoreTests
    {
        private IDocumentStore _docStore;

        [SetUp]
        public void SetUp()
        {
            _docStore = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };
            _docStore.Initialize();
        }


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
                                {2, new Vote {Id=1, UserName = "Ulad", Value ="3"}}
                            }
                        }
                    },
                    {
                        2,
                        new VotingSession
                        {
                            Description = "Do that",
                            VotedAt = DateTime.UtcNow,
                            Votes = new Dictionary<long, Vote>
                            {
                                {1, new Vote {Id = 1, UserName = "Andrei", Value = "5"}}, 
                                {2, new Vote {Id=1, UserName = "Ulad", Value ="3"}}
                            },
                            AgreedVote = "5"
                        }
                    }
                }
            };

            using (var session = _docStore.OpenSession())
            {
                session.Store(room);
                session.SaveChanges();
            }
        }

        [Test]
        public void CanDelete()
        {
            PlanningRoom room;
            using (var repo = new PlanningRoomRavenRepository(_docStore))
            {
                repo.Save(new PlanningRoom());

                room = repo.Query().First();
            }

            using (var repo = new PlanningRoomRavenRepository(_docStore))
            {
                repo.Delete(room);
            }
        }
    }
}