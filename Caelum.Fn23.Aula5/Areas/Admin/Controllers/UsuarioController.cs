using Caelum.Fn23.Curso.Infra;
using Caelum.Fn23.Curso.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caelum.Fn23.Curso.Areas.Admin.Controllers
{
    public class UsuarioController : Controller
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

        // GET: Admin/Usuario
        public ActionResult Index()
        {
            var lista = Manager.Users.ToList();
            return View(lista);
        }
    }
}