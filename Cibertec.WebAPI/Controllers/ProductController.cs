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
    [RoutePrefix("product")]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
            _log.Info($"{typeof(ProductController)} ejecutándose");
        }


        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Products product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productID = _unit.Products.Insert(product);
            return Ok(new { id = productID });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Products product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_unit.Products.Update(product)) return BadRequest("No se actualizó");

            return Ok(new { updated = true });
        }

        [Route("{productID}")]
        public IHttpActionResult Get(int productID)
        {
            if (productID <= 0) return BadRequest();

            return Ok(_unit.Products.GetById(productID));
        }

        [Route("list")]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Products.GetList());
        }
    }
}