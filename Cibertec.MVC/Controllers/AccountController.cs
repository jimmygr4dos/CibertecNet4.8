using Cibertec.MVC.Models;
using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.MVC.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        //No pedimos Autorización para que permita ingresar a esta Action
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View(new UserViewModel { ReturnUrl = returnUrl });
        }

        //No pedimos Autorización para que permita ingresar a esta Action
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var validUser = _unit.Users.ValidaterUser(user.Email, user.Password);

            if (validUser == null)
            {
                ModelState.AddModelError("Error", "¡Usuario o clave no válido!");
                return View(user);
            }

            if(validUser.Estado == "B")
            {
                ModelState.AddModelError("Error", "¡Usuario bloqueado!");
                return View(user);
            }

            var identity = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Email, validUser.Email),
                                                     new Claim(ClaimTypes.Role, validUser.Roles),
                                                     new Claim(ClaimTypes.Name, $"{validUser.FirstName} {validUser.LastName}"),
                                                     new Claim(ClaimTypes.NameIdentifier, validUser.Id.ToString()) }, "ApplicationCookie");

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;
            authManager.SignIn(identity);
            return RedirectToLocal(user.ReturnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Account");
        }
    }
}