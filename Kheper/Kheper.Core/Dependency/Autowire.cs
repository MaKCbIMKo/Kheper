namespace Kheper.Core.Dependency
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class AutowireAttribute : Attribute
    {
        public Type As { get; set; }
    }
}
