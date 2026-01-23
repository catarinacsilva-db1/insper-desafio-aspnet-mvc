using CadastroUsuarios.Data;
using CadastroUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastroUsuarios.Controllers.Utils
{
    public class Validators
    {
        private readonly AppDbContext _db;

        public Validators(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<UsuarioModel> PesquisaUsuario(string filtro, string termoPesquisa)
        {
            IQueryable<UsuarioModel> query = _db.Usuarios;

            if (filtro == "ativo")
                query = query.Where(u => u.Ativo == true);

            else if (filtro == "inativo")
                query = query.Where(u => u.Ativo == false);


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
        public bool ValidaCpfUsuario(int id, string cpf)
        {
            if (id == 0)
            {
                return !_db.Usuarios.Any(u => u.Cpf == cpf);
            }
            return !_db.Usuarios.Any(u => u.Cpf == cpf && u.Id != id);
        }
    }
}