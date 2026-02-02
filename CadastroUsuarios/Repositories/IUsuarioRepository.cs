using CadastroUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuarios.Repositories
{
    public interface IUsuarioRepository : IDisposable
    {
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel BuscarPorId(int id);
        IEnumerable<UsuarioModel> ListarTodos();
        UsuarioModel AtualizarUsuario(UsuarioModel usuario);
        UsuarioModel AtualizaStatusUsuario(int id);
        void Remover(int id);
    }
}
