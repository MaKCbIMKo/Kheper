namespace Kheper.Core.Dependency
{
    public enum ELifetimeScope
    {
        Unspecified = 0,
        InstancePerDependency,
        SingleInstance,
        InstancePerLifetimeScope,
        InstancePerMatchingLifetimeScope,
        InstancePerRequest,
        InstancePerOwned,
        ThreadScope
    }
}