﻿using Caelum.Fn23.Curso.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Caelum.Fn23.Curso.Infra;

[assembly: OwinStartup(typeof(Caelum.Fn23.Curso.App_Start.Startup))]

namespace Caelum.Fn23.Curso.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            var options = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Autenticacao/Login")
            };
            builder.UseCookieAuthentication(options);

            //pendurar o usermanager
            builder.CreatePerOwinContext<BlogContext>(
                () => new BlogContext()
            );
            builder.CreatePerOwinContext<IUserStore<Usuario>>(
                (opt, owinContext) =>
                {
                    var context = owinContext.Get<BlogContext>();
                    return new UserStore<Usuario>(context);
                }
            );
            builder.CreatePerOwinContext<UserManager<Usuario>>(
                (opt, owinContext) =>
                {
                    var store = owinContext.Get<IUserStore<Usuario>>();
                    return new UserManager<Usuario>(store);
                }
            );


        }
    }
}