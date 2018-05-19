using Caelum.Fn23.Curso.DAO;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Caelum.Fn23.Curso.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostDaoEF _dao;

        public HomeController()
        {
            _dao = new PostDaoEF();
        }

        private void FazAlgumaCoisa()
        {
            throw new NotImplementedException();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            var e = filterContext.Exception;
            var viewErro = View("Erro", e);
            filterContext.Result = viewErro;
            filterContext.ExceptionHandled = true;

        }

        public ActionResult Exception()
        {
            FazAlgumaCoisa();
            return new HttpStatusCodeResult(200);
        }

        public ActionResult Index()
        {
            return View(_dao.Lista.Where(p => p.Publicado).ToList());
        }

        public ActionResult Busca(string termo)
        {
            string termoTratado = termo.ToLower();
            var publicados = _dao.Lista
                .Where(p => 
                    (p.Publicado) && 
                    (p.Titulo.ToLower().Contains(termoTratado) || 
                    p.Resumo.ToLower().Contains(termoTratado))
                );

            return View("Index", publicados.ToList());
        }
    }
}