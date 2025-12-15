using CadastroUsuarios.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CadastroUsuarios.Controllers
{
    public class UsuarioController : Controller //herança da classe Controller
    {
        public ActionResult Listar()
        {
            var userList = new List<UsuarioViewModel>();
      
            return View(userList);
        }
        public ActionResult Cadastrar()
        {
            var user = new UsuarioViewModel //instanciando o ViewModel
            {
                Ativo = true //valor padrão para o campo Ativo
            };
            

            return View(user);
        }

        [HttpPost] //tipo de requisição da Action.
        public ActionResult CadastrarPost(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mensagemErro = "Usuário não cadastrado";
                return View("Cadastrar", model);
            }

            TempData["mensagemSucesso"] = "Usuário Cadastrado";  
            return RedirectToAction("Listar");
        }
    }
}