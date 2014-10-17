using Autofac;
using Kheper.Core.Store;
using Kheper.DataAccess.InMemory;

namespace Kheper.Web
{
    using System.Reflection;

    using Autofac.Integration.WebApi;

    public class DependencyConfig
	{
		public static ILifetimeScope CreateContainer()
		{
			var builder = new ContainerBuilder();

		    builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			builder.RegisterType<PlanningRoomInMemoryRepository>().As<IPlanningRoomRepository>().SingleInstance();
            builder.RegisterType<RetrospectiveRoomInMemoryRepository>().As<IRetrospectiveRoomRepository>().SingleInstance();
			builder.RegisterType<VoteInMemoryRepository>().As<IVoteRepository>().SingleInstance();
            builder.RegisterType<VotingSessionInMemoryRepository>().As<IVotingSessionRepository>().SingleInstance();

			return builder.Build();
		}
	}
}