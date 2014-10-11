using Kheper.Core.Model;
using Kheper.Core.Store;

namespace Kheper.DataAccess.InMemory
{
	public class PlanningRoomInMemoryRepository : GenericInMemoryRepository<PlanningRoom, long>, IPlanningRoomRepository
	{
		protected override long GetId(PlanningRoom instance)
		{
			return instance.Id;
		}
	}
}
