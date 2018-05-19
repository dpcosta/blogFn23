namespace Caelum.Fn23.Curso.Migrations
{
    using Caelum.Fn23.Curso.Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Caelum.Fn23.Curso.Infra.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Caelum.Fn23.Curso.Infra.BlogContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //var posts = new List<Post>
            //{
            //    new Post{ Titulo = "Harry Potter 1", Resumo = "E a Pedra Filosofal", Categoria = "Filmes" },
            //    new Post{ Titulo = "Harry Potter 2", Resumo = "E a C�mara Secreta", Categoria = "Filmes"},
            //    new Post{ Titulo = "Harry Potter 3", Resumo = "Prisioneiro de Askaban", Categoria = "Filmes" },
            //    new Post{ Titulo = "Harry Potter 4", Resumo = "C�lice de Fogo", Categoria = "Filmes"},
            //};

            //foreach (var post in posts)
            //{
            //    context.Posts.AddOrUpdate(post);
            //}


            var hasher = new PasswordHasher();
            string passwordHash = hasher.HashPassword("123");

            Usuario admin = new Usuario
            {
                UserName = "admin",
                PasswordHash = passwordHash,
                UltimoLogin = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            context.Users.AddOrUpdate(u => u.UserName, admin);

        }
    }
}
