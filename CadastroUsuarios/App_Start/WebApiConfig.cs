using CadastroUsuarios.Filters;
using System.Web.Http.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CadastroUsuarios
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ApiExceptionFilter());
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
