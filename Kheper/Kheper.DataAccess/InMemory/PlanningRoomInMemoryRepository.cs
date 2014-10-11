using Kheper.Core.Model;
using Kheper.Core.Store;

namespace Kheper.DataAccess.InMemory
{
    public class PlanningRoomInMemoryRepository : GenericInMemoryRepository<PlanningRoom, long>, IPlanningRoomRepository
    {
        private long lastId;

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
