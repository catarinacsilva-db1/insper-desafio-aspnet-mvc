using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Data;
using CadastroUsuarios.Models;
using CadastroUsuarios.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public string mensagemValidacao { get; private set; }

        public UsuarioService()
        {
            _validator = new Validators(_repository);
            _filters = new FiltroQueries(_repository);
        }
        public UsuarioModel Adicionar(UsuarioModel usuarioModel)
        {
            if (!_validator.ValidaCpfUsuario(usuarioModel.Id, usuarioModel.Cpf))
            {
                mensagemValidacao = _validator.MensagemValidacao;
                return (usuarioModel);
            }

            _repository.Adicionar(usuarioModel); 
            return (usuarioModel);
        }
        public UsuarioModel Editar(UsuarioModel usuarioModel)
        {
            if (!_validator.ValidaCpfUsuario(usuarioModel.Id, usuarioModel.Cpf))
            {
                mensagemValidacao = _validator.MensagemValidacao;
                return (usuarioModel);
            }

            _repository.AtualizarUsuario(usuarioModel);
            return (usuarioModel);
        }
        public UsuarioModel EditarStatus(int id)
        {
            return _repository.AtualizaStatusUsuario(id);
        }

        public void Deletar(int id)
        {
            _repository.Remover(id);
        }

        public UsuarioModel BuscarPorId(int id)
        {
            return _repository.BuscarPorId(id);
        }

        public IEnumerable<UsuarioModel> Listar()
        {
            return _repository.ListarTodos();
        }

        public IQueryable<UsuarioModel> PesquisaUsuario(string filtro, string termoPesquisa)
        {
            return _filters.PesquisaUsuario(filtro, termoPesquisa);
        }

    }
}