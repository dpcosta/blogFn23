using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caelum.Fn23.Curso.Models
{
    public class Usuario : IdentityUser
    {
        public DateTime? UltimoLogin { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}