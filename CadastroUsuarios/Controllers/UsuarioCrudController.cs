using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Data;
using CadastroUsuarios.Models;


namespace CadastroUsuarios.Controllers
{
    public class UsuarioCrudController : Controller
    {
        private readonly AppDbContext db;
        private readonly Validators validator;

        public UsuarioCrudController()
        {
            db = new AppDbContext();
            validator = new Validators(db);
        }

        public ActionResult Index(string filtro = "todos", string termoPesquisa = "")
        {
            var query = validator.PesquisaUsuario(filtro, termoPesquisa);

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
            if (!validator.ValidaCpfUsuario(usuarioModel.Id, usuarioModel.Cpf))
            {
                ViewBag.mensagemErro = "Este CPF já está cadastrado";
                return View(usuarioModel);
            }

            db.Usuarios.Add(usuarioModel);
            db.SaveChanges();
            TempData["mensagemSucesso"] = "Usuário Cadastrado";
            return RedirectToAction("Index");    
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel usuarioModel = db.Usuarios.Find(id);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }
            return View(usuarioModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ativo,Nome,Sobrenome,NomeSocial,DataNascimento,Cpf,Senha")] UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mensagemErro = "Usuário não cadastrado";
                return View(usuarioModel);
            }
            if (!validator.ValidaCpfUsuario(usuarioModel.Id, usuarioModel.Cpf))
            {
                ViewBag.mensagemErro = "Este CPF já está cadastrado";
                return View(usuarioModel);
            }

            db.Entry(usuarioModel).State = EntityState.Modified;
            db.SaveChanges();
            TempData["mensagemSucesso"] = "Usuário Atualizado";
            return RedirectToAction("Index");
        }

        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel usuarioModel = db.Usuarios.Find(id);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }
            return View(usuarioModel);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioModel usuarioModel = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarioModel);
            db.SaveChanges();
            TempData["mensagemSucesso"] = "Usuário Excluído";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        

    }
}
