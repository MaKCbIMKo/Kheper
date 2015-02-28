[assembly: Microsoft.Owin.OwinStartup(typeof(Kheper.Web.OwinStartup))]

namespace Kheper.Web
{
    using System.Reflection;
    using System.Threading;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Kheper.DataAccess;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Ninject;
    using Microsoft.Owin.BuilderProperties;

    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    using Owin;

    public class OwinStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var container = CreateContainer();

            // Web Api
            var httpConfiguration = this.ConfigureWebApi(container);
            appBuilder.UseNinjectMiddleware(() => container).UseNinjectWebApi(httpConfiguration);

            // MVC
            this.ConfigureMvcRoutes(RouteTable.Routes, container);

            // SignalR
            var hubConfiguration = new HubConfiguration
            {
                Resolver = new NinjectDependencyResolver(container)
            };
            appBuilder.MapSignalR(hubConfiguration);

            this.RegisterShutdown(appBuilder, container);
        }

        public static IKernel CreateContainer()
        {
            var container = new StandardKernel();
            Assembly[] assemblies =
                {
                    Assembly.GetExecutingAssembly(),
                    typeof(DataAccessAssembly).Assembly
                };
            container.Load(assemblies);
            return container;
        }

        private void RegisterShutdown(IAppBuilder appBuilder, IKernel container)
        {
            var properties = new AppProperties(appBuilder.Properties);
            var token = properties.OnAppDisposing;
            if (token != CancellationToken.None)
            {
                token.Register(() => this.Shutdown(container));
            }
        }

        private void Shutdown(IKernel container)
        {
            container.Dispose();
        }

        internal HttpConfiguration ConfigureWebApi(IKernel container)
        {
            var httpConfiguration = new HttpConfiguration();

            httpConfiguration.MapHttpAttributeRoutes();

            //var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return httpConfiguration;
        }

        internal void ConfigureMvcRoutes(RouteCollection routes, IKernel container)
        {
            routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // DependencyResolver.SetResolver(new Ninject.Web.Mvc.NinjectDependencyResolver(container));
        }
    }
}
