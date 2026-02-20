using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Models;
using CadastroUsuarios.Repositories;
using CadastroUsuarios.Service.Utils;
using System.Linq;

namespace CadastroUsuarios.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly Validators _validator;
        private readonly FiltroQueries _filters;

        public UsuarioService(IUsuarioRepository repository, Validators validator, FiltroQueries filters)
        {
            _repository = repository;
            _validator = validator;
            _filters = filters;
        }

        public UsuarioModel AdicionarUsuario(UsuarioModel usuarioModel)
        {
            _validator.ValidaCamposUsuario(usuarioModel);
            _repository.Adicionar(usuarioModel);
            return (usuarioModel);
        }

        public UsuarioModel EditarUsuario(UsuarioModel usuarioModel)
        {
            _validator.ValidaCamposUsuario(usuarioModel);
            _repository.Atualizar(usuarioModel);
            return (usuarioModel);
        }

        public UsuarioModel EditarStatusUsuario(int id)
        {
            var usuario = BuscarPorId(id);
            return _repository.AtualizaStatus(usuario);
        }

        public void DeletarUsuario(int id)
        {
            var usuario = BuscarPorId(id);
            _repository.Remover(usuario);

        }

        public UsuarioModel BuscarPorId(int id)
        {
            var usuario = _repository.BuscarPorId(id);
            _validator.ValidaBuscaUsuario(usuario);
            return usuario;
        }

        public IQueryable<UsuarioModel> PesquisaUsuario(string filtro, string termoPesquisa)
        {
            return _filters.PesquisaUsuario(filtro, termoPesquisa);
        }

    }
}