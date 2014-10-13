using Kheper.Core.Model;
using Kheper.Core.Store;

namespace Kheper.DataAccess.InMemory
{
	public class RetrospectiveRoomInMemoryRepository : GenericInMemoryRepository<RetrospectiveRoom, long>, IRetrospectiveRoomRepository
	{
	    private long _lastId;

		protected override long? GetId(RetrospectiveRoom instance)
		{
		    return instance.Id == 0 ? (long?)null : instance.Id;
		}

	    protected override void SetId(RetrospectiveRoom instance, long id)
	    {
	        instance.Id = id;
	    }

	    protected override long GenerateId(RetrospectiveRoom instance)
	    {
	        return ++_lastId;
	    }
	}
}
