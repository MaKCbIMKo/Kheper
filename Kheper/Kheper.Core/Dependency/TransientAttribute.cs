using System;

namespace Kheper.Core.Dependency
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TransientAttribute : LifetimeScopeAttribute
    {
        public TransientAttribute()
        {
            Scope = ELifetimeScope.InstancePerDependency;
        }
    }
}
