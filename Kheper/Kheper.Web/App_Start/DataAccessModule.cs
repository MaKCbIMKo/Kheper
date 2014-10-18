namespace Kheper.Web
{
    using System;

    using Autofac;

    using Kheper.Core.Store;
    using Kheper.DataAccess.InMemory;

    public enum DataAccessImplementation
    {
        Memory,
        Raven
    }

    public class DataAccessModule : Module
    {
        public DataAccessImplementation Implementation { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            switch (Implementation)
            {
                case DataAccessImplementation.Memory:
                    builder.RegisterType<PlanningRoomInMemoryRepository>().As<IPlanningRoomRepository>().SingleInstance();
                    builder.RegisterType<RetrospectiveRoomInMemoryRepository>().As<IRetrospectiveRoomRepository>().SingleInstance();
                    builder.RegisterType<VoteInMemoryRepository>().As<IVoteRepository>().SingleInstance();
                    builder.RegisterType<VotingSessionInMemoryRepository>().As<IVotingSessionRepository>().SingleInstance();
                    break;
                default:
                    throw new NotImplementedException("Data access for " + Implementation + " is not configured.");
            }
        }
    }
}