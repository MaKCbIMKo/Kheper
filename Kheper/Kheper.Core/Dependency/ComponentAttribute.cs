namespace Kheper.Core.Dependency
{
    using System;

    /// <summary>
    /// Designates that the annotated class will be registered in Dependency Injection Container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public EAutowiringPrecedence Precedence { get; set; }
    }
}
