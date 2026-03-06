using CadastroUsuarios.Models;
using CadastroUsuarios.Repositories;
using CadastroUsuarios.Service.Utils.Exceptions;
using System;
using System.Linq;

namespace CadastroUsuarios.Controllers.Utils
{
    public class Validators
    {
        private readonly IUsuarioRepository _repository;

        public Validators(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public void ValidaBuscaUsuario(UsuarioModel usuarioModel)
        {
            if (usuarioModel == null)
            {
                throw new ValidacaoException("Usuário não encontrado");
            }
        }

        public void ValidaCamposUsuario(UsuarioModel usuarioModel)
        {
            if (!ValidaCpfUsuario(usuarioModel.Id, usuarioModel.Cpf))
            {
                throw new ValidacaoException("Este CPF já está cadastrado");
            }

            if (!ValidaIdadeUsuario(usuarioModel.DataNascimento))
            {
                throw new ValidacaoException("Data de Nascimento inválida");
            }

            if (!ValidaSenhaUsuario(usuarioModel.Senha))
            {
                throw new ValidacaoException("Senha inválida");
            }
        }

        public bool ValidaCpfUsuario(int id, string cpf)
        {
            if (id == 0)//criar metodo de repositorio para cpf
            {
                return !_repository.ListarTodos().Any(u => u.Cpf == cpf);
            }
            return !_repository.ListarTodos().Any(u => u.Cpf == cpf && u.Id != id);
        }

        public static bool ValidaIdadeUsuario(DateTime dataNascimento)
        {
            var anoLimite = DateTime.Today.Year - 120;
            if (dataNascimento > DateTime.Today || dataNascimento.Year < anoLimite)
            {
                return false;
            }
            return true;
        }

        public static bool ValidaSenhaUsuario(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha) ||
                senha.Length < 6 ||
                !senha.Any(char.IsDigit) ||
                !senha.Any(char.IsUpper) ||
                !senha.Any(char.IsLower))
            {
                return false;
            }
            return true;
        }
    }
}