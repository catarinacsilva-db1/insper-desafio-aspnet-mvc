using CadastroUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuarios.Repositories
{
    public interface IUsuarioRepository
    {
        void Adicionar(UsuarioModel usuario);
        UsuarioModel BuscarPorId(int id);
        IEnumerable<UsuarioModel> ListarTodos();
        void Atualizar(UsuarioModel usuario);
        void Remover(int id);
    }
}
