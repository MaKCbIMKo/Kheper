using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

using Microsoft.Owin.BuilderProperties;

[assembly: OwinStartup(typeof(Kheper.Web.OwinStartup))]

namespace Kheper.Web
{
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Autofac;
    using Autofac.Builder;
    using Autofac.Features.Scanning;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.SignalR;
    using Autofac.Integration.WebApi;

    using Kheper.Core.Dependency;
    using Kheper.Core.Store;
    using Kheper.Core.Util;
    using Kheper.DataAccess;
    using Kheper.DataAccess.InMemory;

    using Raven.Client;
    using Raven.Client.Embedded;

    public class OwinStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var container = CreateContainer();

            // Web Api
            var httpConfiguration = this.ConfigureWebApi(container);
            appBuilder.UseWebApi(httpConfiguration);

            // MVC
            this.ConfigureMvcRoutes(RouteTable.Routes);
            UnityMvcActivator.Start(container);

            // SignalR
            var hubConfiguration = new HubConfiguration
            {
                Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container)
            };
            appBuilder.MapSignalR(hubConfiguration);

            RegisterShutdown(appBuilder, container);
        }

        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            Assembly[] assemblies = { Assembly.GetExecutingAssembly() };

            builder.RegisterApiControllers(assemblies);
            builder.RegisterHubs(assemblies);
            builder.RegisterControllers(assemblies);

            builder.RegisterAssemblyModules(assemblies);

            return builder.Build();
        }

        private void RegisterShutdown(IAppBuilder appBuilder, ILifetimeScope container)
        {
            var properties = new AppProperties(appBuilder.Properties);
            var token = properties.OnAppDisposing;
            if (token != CancellationToken.None)
            {
                token.Register(() => UnityMvcActivator.Shutdown(container));
            }
        }

        internal HttpConfiguration ConfigureWebApi(ILifetimeScope container)
        {
            var httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(container)
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
