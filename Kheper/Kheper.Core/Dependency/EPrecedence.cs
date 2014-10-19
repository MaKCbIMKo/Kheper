namespace Kheper.Core.Dependency
{
    /// <summary>
    /// Defines the levels of precedence.
    /// A component with higher precedence overrides components registered with lower precedence.
    /// </summary>
    public enum EPrecedence
    {
        Unspecified = 0,

        /// <summary>
        /// This is the lowest precedence used for the components of the framework.
        /// </summary>
        Framework = 10,

        /// <summary>
        /// This precedence is used for components from framework extension.
        /// </summary>
        Extension = 20,

        /// <summary>
        /// This is the default precedence used for production components.
        /// </summary>
        Application = 30,

        /// <summary>
        /// This precedence is used to override production component in development.
        /// </summary>
        Development = 40,

        /// <summary>
        /// This precedence is used to override component in test case.
        /// </summary>
        Test = 50
    }
}
