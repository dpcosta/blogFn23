using Caelum.Fn23.Curso.Filtros;
using Caelum.Fn23.Curso.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Caelum.Fn23.Curso
{
    //1. explicar migrations sob perspectiva de bancos diferentes
    //2. habilitar migrations Enable-Migrations
    //3. criar primeira versão do banco Add-Migration
    //4. criar alguns posts no método Seed()
    //5. rodar Update-Database e ver que o banco foi criado, com a tabela e os posts!

    //6. explicar migrations sob perspectiva de atualização na estrutura do banco

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new ExceptionFilter());
        }
    }
}
