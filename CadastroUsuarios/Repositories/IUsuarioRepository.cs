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
        IQueryable<UsuarioModel> ListarTodos();
        UsuarioModel Atualizar(UsuarioModel usuario);
        UsuarioModel AtualizaStatus(UsuarioModel usuario);
        void Remover(UsuarioModel usuario);
        UsuarioModel BuscarPorId(int id);
    }
}
