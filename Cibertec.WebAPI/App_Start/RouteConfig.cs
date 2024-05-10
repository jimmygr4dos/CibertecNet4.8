using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cibertec.WebAPI.App_Start
{
    public static class RouteConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Para poder mapear nuestras rutas personalizadas
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}