using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using System.Web.Http.Cors;
using Gdot.Care.Common.Api;
using Gdot.Care.Common.Api.ErrorHandler;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CareGateway
{
    [ExcludeFromCodeCoverage]
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonSetting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            jsonSetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings = jsonSetting;
            config.MessageHandlers.Add(new ApiHandler());
            config.Filters.Add(new ApiExceptionFilter());
            config.Filters.Add(new ValidateModelAttribute());
        }
    }
}
