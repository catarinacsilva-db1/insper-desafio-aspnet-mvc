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
        public string MensagemValidacao { get; private set; }

        public UsuarioService(IUsuarioRepository repository, Validators validator, FiltroQueries filters)
        {
            _repository = repository;
            _validator = validator;
            _filters = filters;
        }
        public UsuarioModel AdicionarUsuario(UsuarioModel usuarioModel)
        {
            MensagemValidacao = null;

            if (!_validator.ValidaCamposUsuario(usuarioModel))
            {
                MensagemValidacao = _validator.MensagemValidacao;
                return (usuarioModel);
            }

            _repository.Adicionar(usuarioModel);
            return (usuarioModel);
        }
        public UsuarioModel EditarUsuario(UsuarioModel usuarioModel)
        {
            MensagemValidacao = null;

            if (!_validator.ValidaCamposUsuario(usuarioModel))
            {
                MensagemValidacao = _validator.MensagemValidacao;
                return (usuarioModel);
            }

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
            if (!_validator.ValidaBuscaUsuario(usuario))
            {
                MensagemValidacao = _validator.MensagemValidacao;
                return null;
            }

            return usuario;
        }

        public IQueryable<UsuarioModel> PesquisaUsuario(string filtro, string termoPesquisa)
        {
            return _filters.PesquisaUsuario(filtro, termoPesquisa);
        }

    }
}