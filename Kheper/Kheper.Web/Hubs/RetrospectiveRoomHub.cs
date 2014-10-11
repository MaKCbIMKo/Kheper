using Microsoft.AspNet.SignalR.Hubs;

namespace Kheper.Web.Hubs
{
	[HubName("RetrospectiveRoomHub")]
	public class RetrospectiveRoomHub : BaseHub
	{
		public RetrospectiveRoomHub()
		{

		}

		[HubMethodName("Connect")]
		public Result Connect(long retrospectiveRoomId)
		{
			return Result();
		}

		[HubMethodName("Disconnect")]
		public Result Disconnect(long retrospectiveRoomId)
		{
			return Result();
		}
	}
}