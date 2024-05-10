using Cibertec.Models;
using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cibertec.WebAPI.Controllers
{
    [RoutePrefix("employee")]
    public class EmployeeController : BaseController
    {
        public EmployeeController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        [Route("list")]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Employees.GetList());
        }

        [Route("{employeeID}")]
        [HttpGet]
        public IHttpActionResult Get(string employeeID)
        {
            if (employeeID == "") return BadRequest();

            return Ok(_unit.Employees.GetById(employeeID));
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Employees employee)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_unit.Employees.Update(employee)) return BadRequest("No se actualizó");

            return Ok(new { updated = true });
        }

        [Route("{employeeID}")]
        [HttpDelete]
        public IHttpActionResult Delete(int employeeID)
        {
            if (employeeID == 0) return BadRequest();

            return Ok(_unit.Employees.Delete(new Employees { EmployeeID = employeeID } ));
        }


    }
}