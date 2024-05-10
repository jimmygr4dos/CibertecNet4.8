using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.MVC.ActionFilters
{
    public class RegisterActionFilter: ActionFilterAttribute
    {
        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var msg = string.Format("Inicia la ejecución de Controlador {0}, Acción {1}, Hora Inicio {2}", filterContext.Controller.ToString(), filterContext.ActionDescriptor.ActionName, DateTime.Now.ToString());
            log.Debug(msg);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var msg = string.Format("Termina la ejecución de Controlador {0}, Acción {1}, Hora Inicio {2}", filterContext.Controller.ToString(), filterContext.ActionDescriptor.ActionName, DateTime.Now.ToString());
            log.Debug(msg);
            RegisterMessage("Después de ejecutar la acción", filterContext);
        }

        private void RegisterMessage(string message, ControllerContext context)
        {
            string msg = $"{message} en el controlador";
            context.Controller.ViewBag.MessageLog = msg;
        }
    }
}