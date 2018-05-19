using Caelum.Fn23.Curso.DAO;
using Caelum.Fn23.Curso.Infra;
using Caelum.Fn23.Curso.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caelum.Fn23.Curso.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<UsuarioManager>(UsuarioManager.Create);

            var authOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Autenticacao/Login")
            };

            app.UseCookieAuthentication(authOptions);
        }
    }
}