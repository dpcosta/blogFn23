using Caelum.Fn23.Curso.Infra;
using Caelum.Fn23.Curso.Models;
using Caelum.Fn23.Curso.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caelum.Fn23.Curso.Controllers
{
    public class AutenticacaoController : Controller
    {
        private UserManager<Usuario> _userManager;
        public UserManager<Usuario> UserManager
        {
            get
            {
                if (_userManager ==  null)
                {
                    var context = new BlogContext();
                    var userStore = new UserStore<Usuario>(context);
                    _userManager = new UserManager<Usuario>(userStore);
                }
                return _userManager;
            }
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = UserManager.Find(model.LoginName, model.Password);
                if (usuario != null)
                {
                    //autenticar! SignIn();
                    var identity = UserManager.CreateIdentity(usuario, DefaultAuthenticationTypes.ApplicationCookie);
                    var contextoOwin = HttpContext.GetOwinContext();
                    contextoOwin.Authentication.SignIn(identity);
                    return RedirectToAction("Index", "Post", new { Area = "Admin" });
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult Registro(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario();
                usuario.UserName = model.LoginName;
                usuario.Email = model.Email;

                var result = UserManager.Create(usuario, model.Senha);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var erro in result.Errors)
                    {
                        ModelState.AddModelError("", erro);
                    }
                    return View(model);
                }

            }
            return View(model);
        }

        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}