using Kheper.Core.Api.Store;
using Kheper.Core.Model;

namespace Kheper.Core.Store
{
	public class PlanningRoomInMemoryRepository : GenericInMemoryRepository<PlanningRoom, long>, IPlanningRoomRepository
	{
		protected override long GetId(PlanningRoom instance)
		{
			return instance.Id;
		}
	}
}
