using Kheper.Web.Core;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Unity.WebApi;
using Microsoft.Owin.BuilderProperties;

[assembly: OwinStartup(typeof(Kheper.Web.OwinStartup))]

namespace Kheper.Web
{
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    public class OwinStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var container = UnityConfig.CreateContainer();

            // Web Api
            var httpConfiguration = ConfigureHttp(container);
            appBuilder.UseWebApi(httpConfiguration);

            // MVC
            this.ConfigureMvcRoutes(RouteTable.Routes);
            UnityMvcActivator.Start(container);

            // SignalR
            var hubConfiguration = new HubConfiguration
            {
                Resolver = new SignalRUnityDependencyResolver(container)
            };
            appBuilder.MapSignalR(hubConfiguration);

            RegisterShutdown(appBuilder, container);
        }

        private void RegisterShutdown(IAppBuilder appBuilder, IUnityContainer container)
        {
            var properties = new AppProperties(appBuilder.Properties);
            var token = properties.OnAppDisposing;
            if (token != CancellationToken.None)
            {
                token.Register(() => UnityMvcActivator.Shutdown(container));
            }
        }

        internal HttpConfiguration ConfigureHttp(IUnityContainer container)
        {
            var httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new UnityDependencyResolver(container)
            };

            httpConfiguration.MapHttpAttributeRoutes();

            //var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return httpConfiguration;
        }

        internal void ConfigureMvcRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
