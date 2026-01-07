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
            IQueryable<UsuarioModel> query = db.Usuarios; //entender IQueryble

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
                   // || u.Cpf.ToString().Contains(termoPesquisa)
                );
            }

            // Converte para List para retornar à view
            List<UsuarioModel> usuarios = query.ToList();

            // Passa valores para a view (para manter seleções e termos)
            ViewBag.FiltroAtual = filtro;
            ViewBag.TermoPesquisa = termoPesquisa;

            return View(usuarios);
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
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: UsuarioController2/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "Id,Ativo,Nome,Sobrenome,NomeSocial,DataNascimento,Senha")] UsuarioModel usuarioModel)
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
