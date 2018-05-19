using System.Diagnostics;
using System.Web.Mvc;

namespace Caelum.Fn23.Curso.Filtros
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Debug.WriteLine($"Ocorreu o erro: {filterContext.Exception.Message}!");
            if (!filterContext.ExceptionHandled)
            {
                filterContext.Result = new ViewResult {
                    ViewName = "Erro",
                    ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                    {
                        Model = filterContext.Exception
                    }
                };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}