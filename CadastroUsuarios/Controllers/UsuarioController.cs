using CadastroUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CadastroUsuarios.Controllers
{
    public class UsuarioController : Controller //herança da classe Controller
    {
        public ActionResult Listar()
        {
            var listaUsuarios = RetornarUserCadastrados();

            if (TempData["UsuariosNovos"] != null)
            {
                var listaUsuariosNovos = (List<UsuarioViewModel>)TempData["UsuariosNovos"];
                listaUsuarios.AddRange(listaUsuariosNovos);
            }

            TempData.Keep("UsuariosNovos");

            return View(listaUsuarios); //retorna view associada (mesmo nome)
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

            TempData["UsuariosNovos"] = RetornarNovosUsuarios(model);
            TempData["mensagemSucesso"] = "Usuário Cadastrado";  
            return RedirectToAction("Listar");
        }

        private List<UsuarioViewModel> RetornarUserCadastrados()
        {
            return new List<UsuarioViewModel>
            {
                new UsuarioViewModel {Nome = "Ana", Sobrenome = "Silva", DataNascimento = new DateTime(1990, 1, 1) },
                new UsuarioViewModel {Nome = "João", Sobrenome = "Souza", DataNascimento = new DateTime(1985, 5, 15) },
            };
        }

        private List<UsuarioViewModel> RetornarNovosUsuarios(UsuarioViewModel model)
        {
            if (TempData["UsuariosNovos"] == null)
            {
                return new List<UsuarioViewModel>
                {
                    model
                };
            }
            var lista = (List<UsuarioViewModel>)TempData["UsuariosNovos"];
            lista.Add(model);

            return lista;
        }

    }

}