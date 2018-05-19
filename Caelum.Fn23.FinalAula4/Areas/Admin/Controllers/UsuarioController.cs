using Caelum.Fn23.Curso.Models;
using Microsoft.AspNet.Identity;
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

        private UserManager<Usuario> _userManager;
        public UserManager<Usuario> UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    //em vez de criar, recupera do contexto Owin
                    _userManager = HttpContext.GetOwinContext().Get<UserManager<Usuario>>();
                }
                return _userManager;
            }
        }

        // GET: Admin/Usuario
        public ActionResult Index()
        {
            return View(UserManager.Users.ToList());
        }
    }
}