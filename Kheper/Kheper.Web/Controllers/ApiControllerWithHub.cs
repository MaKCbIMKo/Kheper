using System;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Kheper.Web.Controllers
{
	public abstract class ApiControllerWithHub<THub> : ApiControllerWithHub<THub, dynamic> where THub : IHub { }

	public abstract class ApiControllerWithHub<THub, THubInterface> : ApiController
		where THub : IHub
		where THubInterface : class
	{
		protected Lazy<IHubContext<THubInterface>> hub = new Lazy<IHubContext<THubInterface>>(() => GlobalHost.ConnectionManager.GetHubContext<THub, THubInterface>());

		protected IHubContext<THubInterface> Hub { get { return hub.Value; } }
	}
}