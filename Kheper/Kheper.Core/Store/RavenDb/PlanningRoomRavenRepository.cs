using System;
using System.Linq;
using Kheper.Core.Api.Model;
using Kheper.Core.Api.Store;
using Raven.Client;

namespace Kheper.Core.Store.RavenDb
{
    public class PlanningRoomRavenRepository: IPlanningRoomRepository, IDisposable
    {
        private readonly IDocumentSession _session;

        public PlanningRoomRavenRepository(IDocumentStore store)
        {
            _session = store.OpenSession();
        }

        public PlanningRoom Query(long id)
        {
            return _session.Load<PlanningRoom>(id);
        }

        public IQueryable<PlanningRoom> Query()
        {
            return _session.Query<PlanningRoom>();
        }

        public PlanningRoom Save(PlanningRoom instance)
        {
            _session.Store(instance);
            _session.SaveChanges();

            return instance;
        }

        public void Delete(PlanningRoom instance)
        {
            _session.Delete(instance);

            _session.SaveChanges();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
