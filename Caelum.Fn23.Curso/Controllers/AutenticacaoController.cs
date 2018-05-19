using Caelum.Fn23.Curso.Infra;
using Caelum.Fn23.Curso.Models;
using Caelum.Fn23.Curso.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caelum.Fn23.Curso.Controllers
{
    public class AutenticacaoController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var contextoOwin = HttpContext.GetOwinContext();
            contextoOwin.Authentication.SignOut();
            return RedirectToAction("Index", new { controller = "Home" });
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var contextoOwin = HttpContext.GetOwinContext();

            if (ModelState.IsValid)
            {
                var manager = contextoOwin.GetUserManager<UsuarioManager>();
                var usuario = manager.Find(model.LoginName, model.Password);

                if (usuario != null)
                {
                    var identity = manager.CreateIdentity(usuario, DefaultAuthenticationTypes.ApplicationCookie);
                    contextoOwin.Authentication.SignIn(identity);
                }

                return RedirectToAction("Index", "Post", new { area = "Admin" });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(RegistroViewModel model)
        {
            var contextoOwin = HttpContext.GetOwinContext();

            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario
                {
                    UserName = model.LoginName,
                    Email = model.Email,
                    UltimoLogin = DateTime.Now,
                };

                var manager = contextoOwin.GetUserManager<UsuarioManager>();
                var resultado = manager.Create(usuario, model.Senha);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Login");
                } else
                {
                    foreach (var erro in resultado.Errors)
                    {
                        ModelState.AddModelError("", erro);
                    }
                }

            }
            return View(model);
        }
    }
}