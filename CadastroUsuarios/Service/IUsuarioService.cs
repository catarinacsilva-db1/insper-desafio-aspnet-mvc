using CadastroUsuarios.Models;
using System.Linq;

namespace CadastroUsuarios.Service
{
    public interface IUsuarioService
    {
        UsuarioModel AdicionarUsuario(UsuarioModel usuarioModel);
        UsuarioModel EditarUsuario(UsuarioModel usuarioModel);
        UsuarioModel EditarStatusUsuario(int id);
        UsuarioModel BuscarPorId(int id);
        void DeletarUsuario(int id);
        IQueryable<UsuarioModel> PesquisaUsuario(string filtro, string termoPesquisa);
    }
}
