using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

using Microsoft.Owin.BuilderProperties;

[assembly: OwinStartup(typeof(Kheper.Web.OwinStartup))]

namespace Kheper.Web
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Autofac;
    using Autofac.Core;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.SignalR;
    using Autofac.Integration.WebApi;

    using Kheper.Core.Dependency;
    using Kheper.Core.Store;
    using Kheper.Core.Util;
    using Kheper.DataAccess.InMemory;

    public class OwinStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var containerBuilder = CreateContainer();

            // Web Api
            var httpConfiguration = this.ConfigureWebApi(containerBuilder);
            appBuilder.UseWebApi(httpConfiguration);

            // MVC
            this.ConfigureMvcRoutes(RouteTable.Routes);
            UnityMvcActivator.Start(containerBuilder);

            // SignalR
            var hubConfiguration = new HubConfiguration
            {
                Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(containerBuilder)
            };
            appBuilder.MapSignalR(hubConfiguration);

            RegisterShutdown(appBuilder, containerBuilder);
        }

        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            Assembly[] assemblies = { Assembly.GetExecutingAssembly() };
            builder.RegisterApiControllers(assemblies);
            builder.RegisterHubs(assemblies);
            builder.RegisterControllers(assemblies);

            builder.RegisterAssemblyModules(assemblies);

            // TODO: what scope will be used for autowired component?
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => Attribute.IsDefined(t, typeof(AutowireAttribute)))
                .As(t => ((AutowireAttribute)Attribute.GetCustomAttribute(t, typeof(AutowireAttribute))).As);

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
