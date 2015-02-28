namespace Kheper.Core.Dependency
{
    public enum ELifetimeScope
    {
        Unspecified = 0,
        Transient,
        Singleton,
        Request,
        Thread
    }
}