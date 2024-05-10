using Cibertec.Models;
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
    [RoutePrefix("customer")]
    public class CustomerController : BaseController
    {
        public CustomerController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
            _log.Info($"{typeof(CustomerController)} ejecutándose");
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Customers customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customerID = _unit.Customers.Insert(customer);
            return Ok(new { id = customerID });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Customers customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_unit.Customers.Update(customer)) return BadRequest("No se actualizó");

            return Ok(new { updated = true });
        }

        [Route("{customerID}")]
        //HttpGet no es necesario colocarlo, se considera por defecto
        [HttpGet]
        public IHttpActionResult Get(string customerID)
        {
            if (customerID == "") return BadRequest();

            return Ok(_unit.Customers.GetById(customerID));
        }

        [Route("list")]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Customers.GetList());
        }
    }
}