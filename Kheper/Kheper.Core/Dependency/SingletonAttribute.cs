﻿namespace Kheper.Core.Dependency
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SingletonAttribute : LifetimeScopeAttribute
    {
        public SingletonAttribute()
        {
            Scope = ELifetimeScope.SingleInstance;
        }
    }
}
