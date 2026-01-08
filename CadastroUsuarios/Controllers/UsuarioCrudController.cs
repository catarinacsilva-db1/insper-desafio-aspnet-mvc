using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CadastroUsuarios.Data;
using CadastroUsuarios.Models;

namespace CadastroUsuarios.Controllers
{
    public class UsuarioCrudController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index(string filtro = "todos", string termoPesquisa = "")
        {
            var query = PesquisaUsuario(filtro, termoPesquisa);

            List<UsuarioModel> usuarios = query.ToList();

            ViewBag.FiltroAtual = filtro;
            ViewBag.TermoPesquisa = termoPesquisa;

            return View(usuarios);
        }

        public ActionResult Details(int? id)
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

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "Ativo,Nome,Sobrenome,NomeSocial,DataNascimento,Senha")] UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarioModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarioModel);
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
        public ActionResult Edit([Bind(Include = "Id,Ativo,Nome,Sobrenome,NomeSocial,DataNascimento,Senha")] UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarioModel);
        }

        public ActionResult Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioModel usuarioModel = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarioModel);
            db.SaveChanges();
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

        private IQueryable<UsuarioModel> PesquisaUsuario(string filtro, string termoPesquisa)
        {
            IQueryable<UsuarioModel> query = db.Usuarios;

            if (filtro == "ativo")
            {
                query = query.Where(u => u.Ativo == true);
            }
            else if (filtro == "inativo")
            {
                query = query.Where(u => u.Ativo == false);
            }

            if (!string.IsNullOrEmpty(termoPesquisa))
            {
                termoPesquisa = termoPesquisa.Trim().ToLower();
                query = query.Where(u =>
                    u.Nome.ToLower().Contains(termoPesquisa) ||
                    u.Sobrenome.ToLower().Contains(termoPesquisa) ||
                    u.NomeSocial.ToLower().Contains(termoPesquisa)
                );
            }

            return query;
        }
        private bool ValidaUsuario(int id)
        {
            return 1 == 1;
        }


    }
}
