using CadastroUsuarios.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuarios.Service
{
    public interface IUsuarioService
    {
        Task<UsuarioModel> AdicionarUsuarioAsync(UsuarioModel usuarioModel);
        Task<UsuarioModel> EditarUsuarioAsync(UsuarioModel usuarioModel);
        Task<UsuarioModel> EditarStatusUsuarioAsync(int id);
        Task<UsuarioModel> BuscarPorIdAsync(int id);
        Task DeletarUsuarioAsync(int id);
        Task<IQueryable<UsuarioModel>> PesquisaUsuarioAsync(string filtro, string termoPesquisa);
    }
}
