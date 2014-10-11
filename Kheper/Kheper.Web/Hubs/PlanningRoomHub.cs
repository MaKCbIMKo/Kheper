using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Kheper.Web.Hubs
{
	[HubName("PokerRoomHub")]
	public class PlanningRoomHub : Hub { }

	public interface IPlanningRoomHub
	{
		public void M1();
		public void M2();
	}
}