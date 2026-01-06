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

        // GET: UsuarioController2
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }

        // GET: UsuarioController2/Details/5
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

        // GET: UsuarioController2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController2/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ativo,Nome,Sobrenome,NomeSocial,DataNascimento,Senha")] UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarioModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarioModel);
        }

        // GET: UsuarioController2/Edit/5
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

        // POST: UsuarioController2/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: UsuarioController2/Delete/5
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

        // POST: UsuarioController2/Delete/5
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
    }
}
