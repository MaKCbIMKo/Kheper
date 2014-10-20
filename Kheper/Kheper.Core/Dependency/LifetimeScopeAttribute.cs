using System;

namespace Kheper.Core.Dependency
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LifetimeScopeAttribute : Attribute
    {
        public ELifetimeScope Scope { get; set; }
    }
}
