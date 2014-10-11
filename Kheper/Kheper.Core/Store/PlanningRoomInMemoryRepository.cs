using Kheper.Core.Api.Model;
using Kheper.Core.Api.Store;

namespace Kheper.Core.Store
{
	public class PlanningRoomInMemoryRepository : GenericInMemoryRepository<PlanningRoom, long>, IPlanningRoomRepository
	{
	}
}
