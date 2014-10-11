using System;
using System.Linq;
using Kheper.Core.Model;
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

        public IQueryable<PlanningRoom> Query(Func<PlanningRoom, bool> predicate)
        {
            return _session.Query<PlanningRoom>();
        }

        public PlanningRoom Save(PlanningRoom instance)
        {
            _session.Store(instance);
            _session.SaveChanges();

            return instance;
        }

        public void Delete(long id)
        {
            var entity = Query(id);
            _session.Delete(entity);

            _session.SaveChanges();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
