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

			container.RegisterType<IPlanningRoomRepository, PlanningRoomInMemoryRepository>(new ContainerControlledLifetimeManager());
			container.RegisterType<IRetrospectiveRoomRepository,RetrospectiveRoomInMemoryRepository>(new ContainerControlledLifetimeManager());
		    container.RegisterType<IVoteRepository, VoteInMemoryRepository>(new ContainerControlledLifetimeManager());
		    container.RegisterType<IVotingSessionRepository, VotingSessionInMemoryRepository>(new ContainerControlledLifetimeManager());

			return container;
		}
	}
}