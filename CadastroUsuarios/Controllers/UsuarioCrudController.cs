using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Models;
using CadastroUsuarios.Service;
using CadastroUsuarios.Service.Utils.Exceptions;
using CadastroUsuarios.ViewModel;
using System.Collections.Generic;
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
            List<UsuarioViewModel> usuarios = query.Select(UsuarioMapper.ToViewModel).ToList();

            ViewBag.FiltroAtual = filtro;
            ViewBag.TermoPesquisa = termoPesquisa;
            return View(usuarios);
        }

        // apenas retorna view
        public ActionResult Cadastrar()
        {
            return View();
        }


        [HttpPost, ActionName("Cadastrar")]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mensagemErro = "Usuário não cadastrado";
                return View(usuarioViewModel);
            }

            UsuarioModel usuarioModel = UsuarioMapper.ToModel(usuarioViewModel);

            try
            {
                _service.AdicionarUsuario(usuarioModel);
            }
            catch (ValidacaoException ex)
            {
                ViewBag.mensagemErro = ex.Message;
                return View(usuarioViewModel);
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

            UsuarioViewModel usuarioViewModel = UsuarioMapper.ToViewModel(usuarioModel);
            return View(usuarioViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mensagemErro = "Usuário não atualizado";
                return View(usuarioViewModel);
            }

            try
            {
                UsuarioModel usuarioModel = UsuarioMapper.ToModel(usuarioViewModel);
                _service.EditarUsuario(usuarioModel);

            }
            catch (ValidacaoException ex)
            {
                ViewBag.mensagemErro = ex.Message;
                return View(usuarioViewModel);
            }

            TempData["mensagemSucesso"] = "Usuário Atualizado";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.DeletarUsuario(id);

            }
            catch (ValidacaoException ex)
            {
                ViewBag.mensagemErro = ex.Message;
                return RedirectToAction("Index");
            }
            TempData["mensagemSucesso"] = "Usuário Excluído";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("AtualizarStatus")]
        [ValidateAntiForgeryToken]
        public ActionResult AtualizarStatus(int id)
        {
            try
            {
                _service.EditarStatusUsuario(id);

            }
            catch (ValidacaoException ex)
            {
                ViewBag.mensagemErro = ex.Message;
                return RedirectToAction("Index");
            }

            TempData["mensagemSucesso"] = "Status do usuário atualizado";
            return RedirectToAction("Index");
        }
    }
}
