using Kheper.Core.Model;
using Kheper.Core.Store;

namespace Kheper.DataAccess.InMemory
{
	public class RetrospectiveRoomInMemoryRepository : GenericInMemoryRepository<RetrospectiveRoom, long>, IRetrospectiveRoomRepository
	{
		protected override long GetId(RetrospectiveRoom instance)
		{
			return instance.Id;
		}
	}
}
