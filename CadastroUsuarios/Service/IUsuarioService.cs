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
        string MensagemValidacao { get; }

        UsuarioModel AdicionarUsuario(UsuarioModel usuarioModel);
        UsuarioModel EditarUsuario(UsuarioModel usuarioModel);
        UsuarioModel EditarStatusUsuario(int id);
        UsuarioModel BuscarPorId(int id);
        void DeletarUsuario(int id);
        IQueryable<UsuarioModel> PesquisaUsuario(string filtro, string termoPesquisa);
    }
}
