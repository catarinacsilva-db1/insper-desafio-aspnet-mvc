using CadastroUsuarios.Models;
using CadastroUsuarios.ViewModel;

namespace CadastroUsuarios.Controllers.Utils
{
    public static class UsuarioMapper
    {
        public static UsuarioViewModel ToViewModel(UsuarioModel model)
        {
            return new UsuarioViewModel
            {
                Id = model.Id,
                Ativo = model.Ativo,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                NomeSocial = model.NomeSocial,
                DataNascimento = model.DataNascimento,
                Cpf = model.Cpf,
                Senha = model.Senha
            };
        }

        public static UsuarioModel ToModel(UsuarioViewModel viewModel)
        {
            return new UsuarioModel
            {
                Id = viewModel.Id,
                Ativo = viewModel.Ativo,
                Nome = viewModel.Nome,
                Sobrenome = viewModel.Sobrenome,
                NomeSocial = viewModel.NomeSocial,
                DataNascimento = viewModel.DataNascimento,
                Cpf = viewModel.Cpf,
                Senha = viewModel.Senha
            };
        }
    }
}
