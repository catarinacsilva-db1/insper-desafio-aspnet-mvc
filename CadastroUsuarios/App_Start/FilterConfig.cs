using CadastroUsuarios.Filters;
using System.Web;
using System.Web.Mvc;

namespace CadastroUsuarios
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilter
            {
                View = "Error",
                ExceptionType = typeof(System.Exception)
            });
        }
    }
}
