using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace Kheper.Web.Hubs
{
	[HubName("PokerRoomHub")]
	public class PlanningRoomHub : Hub
	{
		public PlanningRoomHub()
		{
			
		}

		public bool Connect(int planningRoomId)
		{
			return true;
		}

		public bool Disconnect(int planningRoomId)
		{
			return true;
		}

		public override Task OnConnected()
		{
			return base.OnConnected();
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			return base.OnDisconnected(stopCalled);
		}

		public override Task OnReconnected()
		{
			return base.OnReconnected();
		}
	}
}