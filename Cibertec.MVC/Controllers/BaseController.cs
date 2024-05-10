using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.MVC.Controllers
{
    //[Authorize(Roles = "profesor")]
    [Authorize]
    public class BaseController: Controller
    {
        protected readonly IUnitOfWork _unit;
        protected readonly ILog _log;

        public BaseController(IUnitOfWork unit, ILog log)
        {
            _unit = unit;
            _log = log;
        }
    }
}