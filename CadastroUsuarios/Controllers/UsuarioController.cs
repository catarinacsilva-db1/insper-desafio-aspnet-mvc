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
                var listaUsuariosNovos = (List<UsuarioModel>)TempData["UsuariosNovos"];
                listaUsuarios.AddRange(listaUsuariosNovos);
            }

            TempData.Keep("UsuariosNovos");

            return View(listaUsuarios); //retorna view associada (mesmo nome)
        }

        public ActionResult Cadastrar()
        {
            var user = new UsuarioModel //instanciando o ViewModel
            {
                Ativo = true //valor padrão para o campo Ativo
            };

            return View(user);
        }

        [HttpPost] //tipo de requisição da Action.
        public ActionResult CadastrarPost(UsuarioModel model)
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

        private List<UsuarioModel> RetornarUserCadastrados()
        {
            return new List<UsuarioModel>
            {
                new UsuarioModel {Nome = "Ana", Sobrenome = "Silva", DataNascimento = new DateTime(1990, 1, 1) },
                new UsuarioModel {Nome = "João", Sobrenome = "Souza", DataNascimento = new DateTime(1985, 5, 15) },
            };
        }

        private List<UsuarioModel> RetornarNovosUsuarios(UsuarioModel model)
        {
            if (TempData["UsuariosNovos"] == null)
            {
                return new List<UsuarioModel>
                {
                    model
                };
            }
            var lista = (List<UsuarioModel>)TempData["UsuariosNovos"];
            lista.Add(model);

            return lista;
        }

    }

}