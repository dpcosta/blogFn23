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
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterFilters(GlobalFilters.Filters);
        }
    }
}
