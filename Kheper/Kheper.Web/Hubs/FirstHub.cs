using Microsoft.AspNet.SignalR;

namespace Kheper.Web.Hubs
{
	public class FirstHub : Hub
	{
		public void Hello()
		{
			Clients.All.hello();
		}
	}
}