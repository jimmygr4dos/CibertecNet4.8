using Cibertec.Models;
using Cibertec.MVC.ActionFilters;
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
    public class CustomerController : BaseController
    {
        //***********************************
        // SIN INYECCIÓN DE DEPENDENCIAS
        //***********************************
        //private readonly IUnitOfWork _unit;

        //public CustomerController()
        //{
        //    _unit = new NorthwindUnitOfWork(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
        //}
        //***********************************

        private NorthwindSP<VmCustomer> _northwindsp;

        public CustomerController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        [RegisterActionFilter]
        public ActionResult Index()
        {
            ViewBag.ModelName = typeof(Customers).Name;
            ViewBag.Dato = "Listado";

            //Si se quiere devolver una vista diferente al Action, en este caso debería ser Index, pero devolvemos Listado
            //return View("Listado", _unit.Customers.GetList());

            //Aquí _log y _unit los está obteniendo de base --> BaseController
            _log.Info("Ejecución del Controlador Customer OK");
            return View(_unit.Customers.GetList());
        }

        [HttpPost]
        public ActionResult Create(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _unit.Customers.Insert(customer);
                return RedirectToAction("Index");
            }
            //return View(customer);
            return PartialView("_Create", customer);
        }

        //Si no se especifica, por defecto, se considera como método GET --> [HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public PartialViewResult Create()
        {
            return PartialView("_Create", new Customers());
        }

        [HttpPost]
        public ActionResult Edit(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _unit.Customers.Update(customer);
                return RedirectToAction("Index");
            }
            //return View(customer);
            return PartialView("_Edit", customer);
        }

        public ActionResult Edit(string id)
        {
            //return View(_unit.Customers.GetById(id));
            return PartialView("_Edit", _unit.Customers.GetById(id));
        }

        public ActionResult Delete(string id)
        {
            //return View(_unit.Customers.GetById(id));
            return PartialView("_Delete", _unit.Customers.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(Customers customer)
        {
            if (_unit.Customers.Delete(customer))
            {
                return RedirectToAction("Index");
            }
            //return View(customer);
            return PartialView("_Delete", customer);
        }

        [ErrorActionFilter]
        public ActionResult Error()
        {
            throw new System.Exception("Prueba de error");
        }

        public ActionResult Lista()
        {
            return View(_unit.Customers.GetList());
        }

        [HttpPost]
        public ActionResult DetalleCliente(string SelectedCustomerId)
        {
            var customer = _unit.Customers.GetById(SelectedCustomerId);
            return PartialView("_DetalleCliente", customer);
        }

        public ActionResult ListaClientes()
        {
            _northwindsp = new NorthwindSP<VmCustomer>(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
            var datos = _northwindsp.GetDataSP("sp_Lista_Clientes");
            return View(datos);
        }

        public ActionResult Details(string id)
        {
            var customer = _unit.Customers.GetById(id);
            return View("DetalleCliente", customer);
        }

        [Route("Customer/List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0)
            {
                return PartialView(new List<Customers>());
            }

            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.Customers.PagedList(startRecord, endRecord));
        }

        [HttpGet]
        [Route("Customer/Count/{rows:int}")]
        public int Count(int rows)
        {
            var totalRecords = _unit.Customers.Count();
            return totalRecords % rows != 0 ? (totalRecords / rows) + 1 : totalRecords / rows;
        }
    }
}