using CadastroUsuarios.Models;
using System.Web.Mvc;

namespace CadastroUsuarios.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cadastrar()
        {
            var user = new UsuarioViewModel();

            return View(user);
        }
    }
}