using Kheper.Web.Core;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Unity.WebApi;

[assembly: OwinStartup(typeof(Kheper.Web.OwinStartup))]

namespace Kheper.Web
{
	public class OwinStartup
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			var httpConfiguration = new HttpConfiguration();
			var hubConfiguration = new HubConfiguration();

			var container = UnityConfig.CreateContainer();

			httpConfiguration.DependencyResolver = new UnityDependencyResolver(container);
			hubConfiguration.Resolver = new SignalRUnityDependencyResolver(container);

			ConfigureHttp(httpConfiguration);
			appBuilder.UseWebApi(httpConfiguration);
			appBuilder.MapSignalR(hubConfiguration);
		}
		
		internal void ConfigureHttp(HttpConfiguration configuration)
		{
			configuration.MapHttpAttributeRoutes();

			//var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
			//jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}
