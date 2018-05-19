using Caelum.Fn23.Curso.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caelum.Fn23.Curso.App_Start
{
    public class RegistroFiltros
    {
        public static void RegistraFiltros(GlobalFilterCollection filtros)
        {
            filtros.Add(new ExceptionFilter());
        }
    }
}