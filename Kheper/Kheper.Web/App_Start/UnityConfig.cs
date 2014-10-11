using Microsoft.Practices.Unity;

namespace Kheper.Web
{
	public class UnityConfig
	{
		public static IUnityContainer CreateContainer()
		{
			var container = new UnityContainer();

			return container;
		}
	}
}