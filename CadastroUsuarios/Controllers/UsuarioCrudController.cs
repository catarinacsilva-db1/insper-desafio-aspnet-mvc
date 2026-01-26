using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Data;
using CadastroUsuarios.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace CadastroUsuarios.Controllers
{
    public class UsuarioCrudController : Controller
    {
        private readonly AppDbContext _db;
        private readonly Validators _validator;

        public UsuarioCrudController()
        {
            _db = new AppDbContext();
            _validator = new Validators(_db);
        }

        public ActionResult Index(string filtro = "todos", string termoPesquisa = "")
        {
            var query = _validator.PesquisaUsuario(filtro, termoPesquisa);

            List<UsuarioModel> usuarios = query.ToList();

            ViewBag.FiltroAtual = filtro;
            ViewBag.TermoPesquisa = termoPesquisa;

            return View(usuarios);
        }

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
            if (!_validator.ValidaCpfUsuario(usuarioModel.Id, usuarioModel.Cpf))
            {
                ViewBag.mensagemErro = "Este CPF já está cadastrado";
                return View(usuarioModel);
            }

            _db.Usuarios.Add(usuarioModel);
            _db.SaveChanges();
            TempData["mensagemSucesso"] = "Usuário Cadastrado";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel usuarioModel = _db.Usuarios.Find(id);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }
            return View(usuarioModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Sobrenome,NomeSocial,DataNascimento,Cpf,Senha")] UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mensagemErro = "Usuário não cadastrado";
                return View(usuarioModel);
            }
            if (!_validator.ValidaCpfUsuario(usuarioModel.Id, usuarioModel.Cpf))
            {
                ViewBag.mensagemErro = "Este CPF já está cadastrado";
                return View(usuarioModel);
            }

            _db.Entry(usuarioModel).State = EntityState.Modified;
            _db.SaveChanges();
            TempData["mensagemSucesso"] = "Usuário Atualizado";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioModel usuarioModel = _db.Usuarios.Find(id);
            _db.Usuarios.Remove(usuarioModel);
            _db.SaveChanges();
            TempData["mensagemSucesso"] = "Usuário Excluído";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("AtualizarStatus")]
        [ValidateAntiForgeryToken]
        public ActionResult AtualizaStatus(int id)
        {
            UsuarioModel usuarioModel = _db.Usuarios.Find(id);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }

            usuarioModel.Ativo = !usuarioModel.Ativo;
            _db.SaveChanges();
            TempData["mensagemSucesso"] = "Status do usuário atualizado";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}
