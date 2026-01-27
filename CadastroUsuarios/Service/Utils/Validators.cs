using CadastroUsuarios.Data;
using CadastroUsuarios.Models;
using CadastroUsuarios.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadastroUsuarios.Controllers.Utils
{
    public class Validators
    {
        private readonly IUsuarioRepository _repository;

        public string MensagemErro { get; private set; }

        public Validators(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        //boleanos temporarios até criar classes de exceçao
        public bool ValidaCamposUsuario(UsuarioModel usuarioModel)
        {
            if (!ValidaCpfUsuario(usuarioModel.Id, usuarioModel.Cpf))
            {
                MensagemErro = "Este CPF já está cadastrado";
                return false;
            }

            if (!ValidaIdadeUsuario(usuarioModel.DataNascimento))
            {
                MensagemErro = "Data de Nascimento inválida";
                return false;
            }
            return true;
        }

        public bool ValidaCpfUsuario(int id, string cpf)
        {
            if (id == 0)
            {
                return !_repository.ListarTodos().Any(u => u.Cpf == cpf);
            }
            return !_repository.ListarTodos().Any(u => u.Cpf == cpf && u.Id != id);
        }

        public static bool ValidaIdadeUsuario(DateTime dataNascimento)
        {
            var anoLimite = DateTime.Today.Year - 120;
            if (dataNascimento.Year > DateTime.Today.Year || dataNascimento.Year < anoLimite)
            {
                return false;
            }
            return true;
        }

        public bool ValidaSenhaUsuario(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha) || 
                senha.Length < 6 || 
                !senha.Any(char.IsDigit) || 
                !senha.Any(char.IsUpper) ||
                !senha.Any(char.IsLower))
            {
                MensagemErro = "Senha inválida";
                return false;
            }
            return true;
        }
    }
}