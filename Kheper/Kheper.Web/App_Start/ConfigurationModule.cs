namespace Kheper.Web
{
    using Kheper.Core.Dependency;

    using Ninject.Modules;

    public class ConfigurationModule : NinjectModule
    {
        public EAutowiringPrecedence Environment { get; set; }

        public override void Load()
        {
            PreemptiveAutowiring.Extend(this.Kernel);
        }
    }
}