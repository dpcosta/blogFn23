using Caelum.Fn23.Curso.Infra;
using Caelum.Fn23.Curso.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Caelum.Fn23.Curso.DAO
{
    public class PostDaoEF : IDataAccessObject<Post>
    {
        private readonly BlogContext _contexto;

        public IList<Post> Lista => this._contexto.Posts.ToList();

        public PostDaoEF()
        {
            this._contexto = new BlogContext();
            this._contexto.Database.Log = LogarSQL;
        }

        private void LogarSQL(string sql)
        {
            Debug.WriteLine(sql);
        }

        public Post BuscaPorId(int id)
        {
            return this._contexto.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void Alterar(Post post)
        {
            this._contexto.Entry(post).State = EntityState.Modified;
            this._contexto.SaveChanges();
        }

        public void Incluir(Post post)
        {
            this._contexto.Posts.Add(post);
            this._contexto.SaveChanges();
        }

        public void Remover(Post post)
        {
            this._contexto.Posts.Remove(post);
            this._contexto.SaveChanges();
        }
    }
}