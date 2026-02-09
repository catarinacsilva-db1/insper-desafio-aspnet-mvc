using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Data;
using CadastroUsuarios.Models;
using CadastroUsuarios.Repositories;
using CadastroUsuarios.Service;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace CadastroUsuarios.Controllers
{
    public class UsuarioCrudController : Controller
    {
        private readonly IUsuarioService _service;

        public UsuarioCrudController(IUsuarioService service)
        {
            _service = service;
        }
        

        public ActionResult Index(string filtro = "todos", string termoPesquisa = "")
        {
            var query = _service.PesquisaUsuario(filtro, termoPesquisa);

            List<UsuarioModel> usuarios = query.ToList();

            ViewBag.FiltroAtual = filtro;
            ViewBag.TermoPesquisa = termoPesquisa;

            return View(usuarios);
        }

        // apenas retorna view
        public ActionResult Cadastrar()
        {
            return View();
        }


        //TODO: mudar de bind para busca por ID
        [HttpPost, ActionName("Cadastrar")]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarPost([Bind(Include = "Ativo,Nome,Sobrenome,NomeSocial,DataNascimento,Cpf,Senha")] UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mensagemErro = "Usuário não cadastrado";
                return View(usuarioModel);
            }
            
            _service.AdicionarUsuario(usuarioModel);

            //validação de serviço temporaria até implementar exceptions
            if (_service.MensagemValidacao != null)
            {
                ViewBag.mensagemErro = _service.MensagemValidacao;
                return View(usuarioModel); 
            }
            TempData["mensagemSucesso"] = "Usuário Cadastrado";
            return RedirectToAction("Index");
            
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel usuarioModel = _service.BuscarPorId(id.Value);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }
            return View(usuarioModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,Sobrenome,NomeSocial,DataNascimento,Cpf,Senha")] UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mensagemErro = "Usuário não cadastrado";
                return View(usuarioModel);
            }

            _service.EditarUsuario(usuarioModel);

            if (_service.MensagemValidacao != null)
            {
                ViewBag.mensagemErro = _service.MensagemValidacao;
                return View(usuarioModel);
            }

            TempData["mensagemSucesso"] = "Usuário Atualizado";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeletarUsuario(id);
            
            if (_service.MensagemValidacao != null)
            {
                ViewBag.mensagemErro = _service.MensagemValidacao;
                return RedirectToAction("Index");
            }

            TempData["mensagemSucesso"] = "Usuário Excluído";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("AtualizarStatus")]
        [ValidateAntiForgeryToken]
        public ActionResult AtualizarStatus(int id)
        {
            _service.EditarStatusUsuario(id);
            
            if (_service.MensagemValidacao != null)
            {
                ViewBag.mensagemErro = _service.MensagemValidacao;
                return RedirectToAction("Index");
            }

            TempData["mensagemSucesso"] = "Status do usuário atualizado";
            return RedirectToAction("Index");
        }
    }
}
