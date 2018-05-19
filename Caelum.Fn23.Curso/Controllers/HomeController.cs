using Caelum.Fn23.Curso.DAO;
using Caelum.Fn23.Curso.Models;
using System.Linq;
using System.Web.Mvc;

namespace Caelum.Fn23.Curso.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataAccessObject<Post> _dao;

        public HomeController(IDataAccessObject<Post> dao)
        {
            _dao = dao;
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