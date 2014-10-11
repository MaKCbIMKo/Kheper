using System.Collections.Generic;
using System.Web.Http;

namespace Kheper.Web.Controllers
{
	[RoutePrefix("api/planning")]
	public class PlanningController : ApiController
	{
		[Route]
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}
	}
}