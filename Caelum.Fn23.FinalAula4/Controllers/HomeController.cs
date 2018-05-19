using Caelum.Fn23.Curso.DAO;
using Caelum.Fn23.Curso.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caelum.Fn23.Curso.Controllers
{
    public class HomeController : Controller
    {
        private IDataAccessObject<Post> _dao;
        public IDataAccessObject<Post> Dao
        {
            get
            {
                if (_dao == null)
                {
                    _dao = HttpContext.GetOwinContext().Get<IDataAccessObject<Post>>();
                }
                return _dao;
            }
        }

        public void FazerAlgumaCoisa()
        {
            throw new NotImplementedException();
        }

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    base.OnException(filterContext);

        //    var viewErro = new ViewResult
        //    {
        //        ViewName = "Erro",
        //        ViewData = new ViewDataDictionary(this.ViewData)
        //        {
        //            Model = filterContext.Exception
        //        }
        //    };

        //    filterContext.Result = viewErro;
        //    filterContext.ExceptionHandled = true;
        //}

        public ActionResult Exception()
        {
            FazerAlgumaCoisa();
            return new HttpStatusCodeResult(200);
            //try
            //{
            //    FazerAlgumaCoisa();
            //    return new HttpStatusCodeResult(200);
            //}
            //catch (Exception e)
            //{
            //    //mostrar uma página dizendo que não foi possível completar a operação
            //    //enviar email para administradores com info do erro
            //    //logar o erro em arquivo para consulta futura
            //    var viewErro = new ViewResult
            //    {
            //        ViewName = "Erro",
            //        ViewData = new ViewDataDictionary(this.ViewData)
            //        {
            //            Model = e
            //        }
            //    };

            //    HttpContext.Response.StatusCode = 500;
            //    return viewErro;
            //}
        }

        public ActionResult Index()
        {
            return View(Dao.Lista.Where(p => p.Publicado).ToList());
        }

        public ActionResult Busca(string termo)
        {
            string termoTratado = termo.ToLower();
            var publicados = Dao.Lista
                .Where(p => 
                    (p.Publicado) && 
                    (p.Titulo.ToLower().Contains(termoTratado) || 
                    p.Resumo.ToLower().Contains(termoTratado))
                );

            return View("Index", publicados.ToList());
        }
    }
}