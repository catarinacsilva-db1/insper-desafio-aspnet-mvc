using CadastroUsuarios.Models;
using CadastroUsuarios.Repositories;
using System.Linq;

namespace CadastroUsuarios.Service.Utils
{
    public class FiltroQueries
    {
        private readonly IUsuarioRepository _repository;
        public FiltroQueries(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<UsuarioModel> PesquisaUsuario(string filtro, string termoPesquisa)
        {
            IQueryable<UsuarioModel> query = _repository.ListarTodos();

            if (filtro == "ativo")
                query = query.Where(u => u.Ativo);

            else if (filtro == "inativo")
                query = query.Where(u => !u.Ativo);


            if (!string.IsNullOrEmpty(termoPesquisa))
            {
                termoPesquisa = termoPesquisa.Trim().ToLower();
                query = query.Where(u =>
                    u.Nome.ToLower().Contains(termoPesquisa) ||
                    u.Sobrenome.ToLower().Contains(termoPesquisa) ||
                    u.NomeSocial.ToLower().Contains(termoPesquisa) ||
                    u.Cpf.Contains(termoPesquisa)
                );
            }

            return query;
        }
    }
}