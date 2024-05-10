using Cibertec.Models;
using Cibertec.MVC.Models;
using Cibertec.Repositories.Dapper.Northwind;
using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.MVC.Controllers
{
    public class ProductController : BaseController
    {
        //private readonly IUnitOfWork _unit;

        private NorthwindSP<VmProduct> _northwindsp;

        public ProductController(IUnitOfWork unit, ILog log): base(unit, log)
        {
            //_unit = unit;
        }

        public ActionResult Index()
        {
            ViewBag.ModelName = typeof(Products).Name;
            ViewBag.Dato = "Listado";
            return View(_unit.Products.GetList());
        }

        [HttpPost]
        public ActionResult Create(Products product)
        {
            if (ModelState.IsValid)
            {
                _unit.Products.Insert(product);
                RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Products product)
        {
            if (ModelState.IsValid)
            {
                _unit.Products.Update(product);
                RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            return View(_unit.Products.GetById(id));
        }

        public ActionResult Delete(int id)
        {
            return View(_unit.Products.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(Products product)
        {
            if (_unit.Products.Delete(product))
            {
                RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Lista()
        {
            return View(_unit.Products.GetList());
        }

        [HttpPost]
        public ActionResult DetalleProducto(int SelectedProductId)
        {
            _northwindsp = new NorthwindSP<VmProduct>(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
            var datos = _northwindsp.GetDataSP("sp_Lista_Productos", new { @ProductID = SelectedProductId});
            return PartialView("_DetalleProducto", datos);
        }
    }
}