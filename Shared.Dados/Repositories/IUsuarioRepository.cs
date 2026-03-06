using CadastroUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuarios.Repositories
{
    public interface IUsuarioRepository : IDisposable
    {
        Task<UsuarioModel> AdicionarAsync(UsuarioModel usuario);
        Task<List<UsuarioModel>> ListarTodosAsync();
        Task<UsuarioModel> AtualizarAsync(UsuarioModel usuario);
        Task<UsuarioModel> AtualizaStatusAsync(UsuarioModel usuario);
        Task RemoverAsync(UsuarioModel usuario);
        Task<UsuarioModel> BuscarPorIdAsync(int id);
        Task<bool> ExisteCpfAsync(string cpf, int id = 0);
    }
}
