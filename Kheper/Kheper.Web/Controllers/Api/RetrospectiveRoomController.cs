using System.Collections.Generic;
using System.Web.Http;
using Kheper.Web.Hubs;

namespace Kheper.Web.Controllers
{
	[RoutePrefix("api/retrospective")]
	public class RetrospectiveRoomController : ApiControllerWithHub<RetrospectiveRoomHub>
	{
		[Route]
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new[] { "value1", "value2" };
		}
	}
}