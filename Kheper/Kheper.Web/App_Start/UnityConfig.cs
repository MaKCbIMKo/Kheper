using Kheper.Core.Store;
using Kheper.DataAccess.InMemory;
using Microsoft.Practices.Unity;

namespace Kheper.Web
{
	public class UnityConfig
	{
		public static IUnityContainer CreateContainer()
		{
			var container = new UnityContainer();

			container.RegisterInstance<IPlanningRoomRepository>(new PlanningRoomInMemoryRepository());
			container.RegisterInstance<IRetrospectiveRoomRepository>(new RetrospectiveRoomInMemoryRepository());

			return container;
		}
	}
}