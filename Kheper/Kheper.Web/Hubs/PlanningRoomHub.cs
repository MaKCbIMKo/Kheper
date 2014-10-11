using Microsoft.AspNet.SignalR.Hubs;

namespace Kheper.Web.Hubs
{
	[HubName("PokerRoomHub")]
	public class PlanningRoomHub : BaseHub
	{
		public PlanningRoomHub()
		{
			
		}
		
		[HubMethodName("Connect")]
		public Result Connect(long planningRoomId)
		{
			return Result();
		}

		[HubMethodName("Disconnect")]
		public Result Disconnect(long planningRoomId)
		{
			return Result();
		}
	}
}