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
        

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            _db.Usuarios.Add(usuario);
            _db.SaveChanges();
            return usuario;
        }

        public UsuarioModel AtualizarUsuario(UsuarioModel usuario)
        {
            var usuarioExistente = BuscarPorId(usuario.Id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Sobrenome = usuario.Sobrenome;
                usuarioExistente.NomeSocial = usuario.NomeSocial;
                usuarioExistente.DataNascimento = usuario.DataNascimento;
                usuarioExistente.Cpf = usuario.Cpf;
                usuarioExistente.Senha = usuario.Senha;

                _db.SaveChanges();
                return usuarioExistente;
            }
            return usuario;
        }

        public UsuarioModel AtualizaStatusUsuario(int id)
        {
            var usuario = BuscarPorId(id);
            if (usuario != null)
            {
                usuario.Ativo = !usuario.Ativo;
                _db.SaveChanges();
                return usuario;
            }
            return usuario;
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

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}