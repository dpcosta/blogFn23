using Caelum.Fn23.Curso.Infra;
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
        // GET: Admin/Usuario
        public ActionResult Index()
        {
            var contextoOwin = HttpContext.GetOwinContext();
            var manager = contextoOwin.GetUserManager<UsuarioManager>();
            var lista = manager.Users.ToList();
            return View(lista);
        }
    }
}