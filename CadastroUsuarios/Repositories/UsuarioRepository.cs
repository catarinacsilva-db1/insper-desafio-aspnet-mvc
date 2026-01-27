using CadastroUsuarios.Data;
using CadastroUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastroUsuarios.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _db;
        public UsuarioRepository(AppDbContext db)
        {
            _db = db;   
        }

        public void Adicionar(UsuarioModel usuario)
        {
            _db.Usuarios.Add(usuario);
            _db.SaveChanges();
        }

        public void Atualizar(UsuarioModel usuario)
        {
            var usuarioExistente = BuscarPorId(usuario.Id);
            if (usuarioExistente != null)
            {
                //TODO: fazer mapeamento dos campos
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Senha = usuario.Senha;

                _db.SaveChanges();
            }
        }

        public IEnumerable<UsuarioModel> ListarTodos()
        {
            return _db.Usuarios.ToList();
        }

        public void Remover(int id)
        {
            var usuario = BuscarPorId(id);
            if (usuario != null)
            {
                _db.Usuarios.Remove(usuario);
                _db.SaveChanges();
            }
        }
        public UsuarioModel BuscarPorId(int id)
        {
            return _db.Usuarios.Find(id);
        }
    }
}