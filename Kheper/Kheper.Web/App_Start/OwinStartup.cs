using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using Unity.WebApi;

[assembly: OwinStartup(typeof(Kheper.Web.OwinStartup))]

namespace Kheper.Web
{
	public class OwinStartup
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			var httpConfiguration = new HttpConfiguration();

			var container = UnityConfig.CreateContainer();

			httpConfiguration.DependencyResolver = new UnityDependencyResolver(container);

			ConfigureHttp(httpConfiguration);
			appBuilder.UseWebApi(httpConfiguration);
		}

		internal void ConfigureHttp(HttpConfiguration configuration)
		{
			configuration.MapHttpAttributeRoutes();

			//var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
			//jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}
