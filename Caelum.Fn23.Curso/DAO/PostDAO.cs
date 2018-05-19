using Caelum.Fn23.Curso.Infra;
using Caelum.Fn23.Curso.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Caelum.Fn23.Curso.DAO
{
    public class PostDAO : IDataAccessObject<Post>, IDisposable
    {
        private readonly IDbConnection _cnx;

        public PostDAO()
        {
            this._cnx = ConnectionFactory.CriaConexaoAberta();
        }

        public void Dispose()
        {
            _cnx.Close();
        }

        private void AdicionaParametro(IDbCommand comando, string paramName, object paramValue)
        {
            var param = comando.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            comando.Parameters.Add(param);
        }

        private Post FromDataReader(IDataReader leitor)
        {
            return new Post
            {
                Id = Convert.ToInt32(leitor["Id"]),
                Titulo = Convert.ToString(leitor["Titulo"]),
                Resumo = Convert.ToString(leitor["Resumo"]),
                Categoria = Convert.ToString(leitor["Categoria"])
            };
        }

        public IList<Post> Lista
        {
            get
            {
                var lista = new List<Post>();
                using (var cnx = ConnectionFactory.CriaConexaoAberta())
                {
                    var select = cnx.CreateCommand();
                    select.CommandText = "select * from posts";
                    using (var leitor = select.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            var post = this.FromDataReader(leitor);
                            lista.Add(post);
                        }
                    }
                }
                return lista;
            }
        }

        public Post BuscaPorId(int id)
        {
            Post post = null;
            using (var cnx = ConnectionFactory.CriaConexaoAberta())
            {
                var select = cnx.CreateCommand();
                select.CommandText = "select * from posts where id = @id";
                this.AdicionaParametro(select, "id", id);
                using (var leitor = select.ExecuteReader())
                {
                    if (leitor.Read())
                    {
                        post = this.FromDataReader(leitor);
                    }
                }
            }
            return post;
        }

        public void Incluir(Post post)
        {
            using (var insert = _cnx.CreateCommand())
            {
                insert.CommandText = $@"
                    insert into posts (titulo, resumo, categoria)
                    values (@titulo, @resumo, @categoria)";

                this.AdicionaParametro(insert, "titulo", post.Titulo);
                this.AdicionaParametro(insert, "resumo", post.Resumo);
                this.AdicionaParametro(insert, "categoria", post.Categoria);

                insert.ExecuteNonQuery();
            }
        }

        public void Remover(Post post)
        {
            using (var delete = _cnx.CreateCommand())
            {
                delete.CommandText = "delete from posts where id = @id";

                this.AdicionaParametro(delete, "id", post.Id);

                delete.ExecuteNonQuery();
            }
        }

        public void Alterar(Post post)
        {
            using (var update = _cnx.CreateCommand())
            {
                update.CommandText = @"update posts set 
                        titulo = @titulo,
                        resumo = @resumo,
                        categoria = @categoria
                    where id = @id";

                this.AdicionaParametro(update, "titulo", post.Titulo);
                this.AdicionaParametro(update, "resumo", post.Resumo);
                this.AdicionaParametro(update, "categoria", post.Categoria);
                this.AdicionaParametro(update, "id", post.Id);

                update.ExecuteNonQuery();

            }
        }
    }
}