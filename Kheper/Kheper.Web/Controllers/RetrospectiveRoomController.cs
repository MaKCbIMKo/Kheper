using System.Collections.Generic;
using System.Web.Http;

namespace Kheper.Web.Controllers
{
	[RoutePrefix("api/retrospective")]
	public class RetrospectiveRoomController : ApiController
	{
		[Route]
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new[] { "value1", "value2" };
		}
	}
}