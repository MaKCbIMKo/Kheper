namespace Kheper.Core.Dependency
{
    public enum EPrecedence
    {
        Unspecified = 0,
        Framework,
        Extension,
        Application,
        Development,
        Test
    }
}
