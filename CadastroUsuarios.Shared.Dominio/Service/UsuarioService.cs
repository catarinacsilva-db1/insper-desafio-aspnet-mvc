using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Models;
using CadastroUsuarios.Repositories;
using CadastroUsuarios.Service.Utils;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<UsuarioModel> AdicionarUsuarioAsync(UsuarioModel usuarioModel)
        {
            await _validator.ValidaCamposUsuario(usuarioModel);
            await _repository.AdicionarAsync(usuarioModel);
            return (usuarioModel);
        }

        public async Task<UsuarioModel> EditarUsuarioAsync(UsuarioModel usuarioModel)
        {
            await _validator.ValidaCamposUsuario(usuarioModel);
            await _repository.AtualizarAsync(usuarioModel);
            return (usuarioModel);
        }

        public async Task<UsuarioModel> EditarStatusUsuarioAsync(int id)
        {
            var usuario = await BuscarPorIdAsync(id);
            return await _repository.AtualizaStatusAsync(usuario);
        }

        public async Task DeletarUsuarioAsync(int id)
        {
            var usuario = await BuscarPorIdAsync(id);
            await _repository.RemoverAsync(usuario);

        }

        public async Task<UsuarioModel> BuscarPorIdAsync(int id)
        {
            var usuario = await _repository.BuscarPorIdAsync(id);
            _validator.ValidaBuscaUsuario(usuario);
            return usuario;
        }

        public async Task<IQueryable<UsuarioModel>> PesquisaUsuarioAsync(string filtro, string termoPesquisa)
        {
            return await _filters.PesquisaUsuarioAsync(filtro, termoPesquisa);
        }

    }
}