namespace Kheper.Core.Dependency
{
    using System;

    //[AttributeUsage(AttributeTargets.Class)]
    public class AutowireAttribute : Attribute
    {
        public Type Service { get; private set; }
        public EPrecedence Precedence { get; set; }

        public AutowireAttribute(Type service)
        {
            Service = service;
            Precedence = EPrecedence.Application;
        }
    }
}
