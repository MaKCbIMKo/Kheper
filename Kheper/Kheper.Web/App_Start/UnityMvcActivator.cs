using Kheper.Web;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UnityMvcActivator), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(UnityMvcActivator), "Shutdown")]

namespace Kheper.Web
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Mvc;

    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityMvcActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start(IUnityContainer container) 
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown(IUnityContainer container)
        {
            container.Dispose();
        }
    }
}