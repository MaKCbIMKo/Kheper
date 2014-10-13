using System;
using System.Linq;
using Kheper.Core.Model;
using Kheper.Core.Store;
using Raven.Client;

namespace Kheper.DataAccess.RavenDb
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
            if (instance == null)
                throw new ArgumentNullException("instance");
            if (instance.Id == 0)
                throw new StoreException("Deleting Instance must have non zero ID");

            instance = _session.Load<PlanningRoom>(instance.Id); // We must reload this instance into current session to be able to delete it
            _session.Delete(instance);
            _session.SaveChanges();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
