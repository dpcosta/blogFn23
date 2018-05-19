using Caelum.Fn23.Curso.Models;
using Caelum.Fn23.Curso.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Mvc;

namespace Caelum.Fn23.Curso.Controllers
{
    public class AutenticacaoController : Controller
    {
        private UserManager<Usuario> _manager;
        public UserManager<Usuario> Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = HttpContext
                        .GetOwinContext()
                        .Get<UserManager<Usuario>>();
                }
                return _manager;
            }
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = Manager.Find(model.LoginName, model.Password);
                if (usuario != null)
                {
                    var identity = Manager.CreateIdentity(
                        usuario, 
                        DefaultAuthenticationTypes.ApplicationCookie
                    );
                    HttpContext.GetOwinContext().Authentication.SignIn(identity);
                    usuario.UltimoLogin = DateTime.Now;
                    var resultado = Manager.Update(usuario);
                    if (resultado.Succeeded)
                    {
                        return RedirectToAction(
                            "Index",
                            new { Area = "Admin", Controller = "Post" }
                        );
                    }
                    else
                    {
                        HttpContext.GetOwinContext().Authentication.SignOut();
                        return RedirectToAction("Login");
                    }
                }
                
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
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario
                {
                    UserName = model.LoginName,
                    Email = model.Email
                };
                var resultado = Manager.Create(usuario, model.Senha);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var erro in resultado.Errors)
                    {
                        ModelState.AddModelError("", erro);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}