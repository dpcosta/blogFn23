using Caelum.Fn23.Curso.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Caelum.Fn23.Curso.DAO;

[assembly : OwinStartup(typeof(Caelum.Fn23.Curso.Infra.Startup))]

namespace Caelum.Fn23.Curso.Infra
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            var options = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login")
            };

            builder.UseCookieAuthentication(options);

            //injetando objetos no contexto Owin, sem precisar de Ninject!
            builder.CreatePerOwinContext<BlogContext>(() => new BlogContext());
            builder.CreatePerOwinContext<IUserStore<Usuario>>(
                (opt, owinContext) =>
                {
                    var contexto = owinContext.Get<BlogContext>();
                    return new UserStore<Usuario>(contexto);
                }
            );
            builder.CreatePerOwinContext<UserManager<Usuario>>(
                (opt, owinContext) =>
                {
                    var store = owinContext.Get<IUserStore<Usuario>>();
                    return new UserManager<Usuario>(store);
                }
            );
            builder.CreatePerOwinContext<IDataAccessObject<Post>>(
                (opt, owinContext) =>
                {
                    var contexto = owinContext.Get<BlogContext>();
                    return new PostDaoEF(contexto);
                }
            );

        }
    }
}