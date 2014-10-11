using Kheper.Core.Api.Model;
using Kheper.Core.Api.Store;

namespace Kheper.Core.Store
{
	public class RetrospectiveRoomInMemoryRepository : GenericInMemoryRepository<RetrospectiveRoom, long>, IRetrospectiveRoomRepository
	{
		protected override long GetId(RetrospectiveRoom instance)
		{
			return instance.Id;
		}
	}
}
