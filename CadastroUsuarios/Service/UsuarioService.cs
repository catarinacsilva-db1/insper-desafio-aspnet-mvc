using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Data;
using CadastroUsuarios.Models;
using CadastroUsuarios.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadastroUsuarios.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly Validators _validator;
        private readonly FiltroQueries _filters;

        public UsuarioService()
        {
            _validator = new Validators(_repository);
            _filters = new FiltroQueries(_repository);
        }
        public UsuarioModel Adicionar(UsuarioModel usuarioModel)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public UsuarioModel Editar(UsuarioModel usuarioModel)
        {
            throw new NotImplementedException();
        }

        public UsuarioModel BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioModel> Listar()
        {
            throw new NotImplementedException();
        }

        public IQueryable<UsuarioModel> PesquisaUsuario(string filtro, string termoPesquisa)
        {
            return _filters.PesquisaUsuario(filtro, termoPesquisa);
        }
    }
}