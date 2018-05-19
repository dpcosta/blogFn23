using Caelum.Fn23.Curso.DAO;
using Caelum.Fn23.Curso.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Caelum.Fn23.Curso.Areas.Admin.Controllers
{
    [Authorize]
    public class PostController : Controller
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

        public ActionResult Index()
        {
            return View(Dao.Lista);
        }

        public ActionResult Novo()
        {
            return View("Form");
        }

        public ActionResult Detalhe(int id)
        {
            Post model = Dao.BuscaPorId(id);
            if (model != null)
            {
                return View("Form", model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            Post post = Dao.BuscaPorId(id);
            if (post != null)
            {
                Dao.Remover(post);
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Adiciona(Post post)
        {
            if (ModelState.IsValid)
            {
                post.AutorId = User.Identity.GetUserId();
                Dao.Incluir(post);
                return RedirectToAction("Index");
            }
            //Bad Request
            HttpContext.Response.StatusCode = 400;
            return View("Form", post);
        }

        [HttpPost]
        public ActionResult Altera(Post post)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;
                return View("Form", post);
            }
            if (post.Publicado && (post.DataPublicacao == null))
            {
                post.DataPublicacao = DateTime.Now;
            }
            else if(!post.Publicado && (post.DataPublicacao != null))
            {
                post.DataPublicacao = null;
            }
            Dao.Alterar(post);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Publica(int id)
        {
            var post = Dao.BuscaPorId(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            post.Publicado = true;
            post.DataPublicacao = DateTime.Now;
            Dao.Alterar(post);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CategoriaAutoComplete(string term)
        {
            var categorias = Dao.Lista
                .Where(p => p.Categoria.ToLower().Contains(term.ToLower()))
                .Select(p => new { label = p.Categoria })
                .Distinct()
                .ToList();
            return Json(categorias);
        }
    }
}