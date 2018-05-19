using Caelum.Fn23.Curso.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Caelum.Fn23.Curso.Infra
{
    public class BlogContext : IdentityDbContext
    {
        public DbSet<Post> Posts { get; set; }

        public BlogContext() : base("name=blog")
        { }
    }
}