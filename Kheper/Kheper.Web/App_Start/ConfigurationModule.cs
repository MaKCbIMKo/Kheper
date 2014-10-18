namespace Kheper.Web
{
    using System.Reflection;

    using Autofac;

    using Kheper.Core.Dependency;
    using Kheper.DataAccess;

    public class ConfigurationModule : Autofac.Module
    {
        public EPrecedence Environment { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            Assembly[] assemblies =
                {
                    Assembly.GetExecutingAssembly(),
                    typeof(DataAccessAssembly).Assembly
                };
            builder.RegisterSource(new PreemptiveAutowiring(EPrecedence.Development, assemblies));
        }
    }
}