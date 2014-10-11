using System.Web.Http;
using Kheper.Core.Api.Store;
using Kheper.Core.Model;
using Kheper.Core.Store;

namespace Kheper.Web.Controllers
{
	[RoutePrefix("api/planning")]
	public class PlanningRoomController : ApiController
	{
		private readonly IPlanningRoomRepository _repository;

		public PlanningRoomController(IPlanningRoomRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[Route("{id}")]
		public PlanningRoom Get(long id)
		{
			return _repository.Query(id);
		}
	}
}