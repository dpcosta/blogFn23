using Caelum.Fn23.Curso.Filtros;
using System;
using System.Web.Mvc;

namespace Caelum.Fn23.Curso
{
    internal class FilterConfig
    {
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilter());
        }
    }
}