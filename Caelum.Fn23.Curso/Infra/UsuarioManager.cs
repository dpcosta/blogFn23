using Caelum.Fn23.Curso.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caelum.Fn23.Curso.Infra
{
    public class UsuarioManager : UserManager<Usuario>
    {
        public UsuarioManager(IUserStore<Usuario> store) : base(store)
        {
        }

        public static UsuarioManager Create()
        {
            var userStore = new UserStore<Usuario>(new BlogContext());
            return new UsuarioManager(userStore);
        }
    }
}