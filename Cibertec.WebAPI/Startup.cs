using Cibertec.WebAPI.App_Start;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(Cibertec.WebAPI.Startup))]

namespace Cibertec.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();
            var log = log4net.LogManager.GetLogger(typeof(Startup));
            log.Debug("¡Logger iniciado!");

            var config = new HttpConfiguration();

            //Inyección de Dependencias
            DIConfig.ConfigureInjector(config);

            //Uso de Tokens
            TokenConfig.ConfigureOAuth(app, config);

            //Gestión de rutas
            RouteConfig.Register(config);

            //Optimización de los request del API
            WebApiConfig.Configure(config);

            //CORS
            app.UseCors(CorsOptions.AllowAll);

            app.UseWebApi(config);
        }
    }
}
