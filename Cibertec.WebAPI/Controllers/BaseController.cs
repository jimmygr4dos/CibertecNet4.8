using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cibertec.WebAPI.Controllers
{
    [Authorize]
    public class BaseController : ApiController
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