using CadastroUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuarios.Service
{
    public interface IUsuarioService
    {
        UsuarioModel Adicionar(UsuarioModel usuarioModel);
        UsuarioModel Editar(UsuarioModel usuarioModel);
        void Deletar(int id);
        IEnumerable<UsuarioModel> Listar();
    }
}
