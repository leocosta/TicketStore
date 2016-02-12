using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using TicketStore.API.Handlers;
using TicketStore.Domain.Users;

namespace TicketStore.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var corsAttr = new EnableCorsAttribute(
                ConfigurationManager.AppSettings["EnableCords.Origins"],
                ConfigurationManager.AppSettings["EnableCords.Headers"],
                ConfigurationManager.AppSettings["EnableCords.Methods"],
                ConfigurationManager.AppSettings["EnableCords.ExposedHeaders"]);
            config.EnableCors(corsAttr);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                  name: "DefaultApiAction",
                  routeTemplate: "api/{controller}/{parentId}/{action}/{id}",
                  defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new TokenValidationHandler(config.DependencyResolver.GetService(typeof(AuthenticationService)) as AuthenticationService));

            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.Indent = true;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
