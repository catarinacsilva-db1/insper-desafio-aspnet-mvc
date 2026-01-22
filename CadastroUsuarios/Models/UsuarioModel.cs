using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadastroUsuarios.Models
{
    public class UsuarioModel {

        public int Id { get; set; }

        [Display(Name = "Status")]
        public bool Ativo { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome do usuário")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Sobrenome { get; set; }

        [StringLength(100)]
        [Display(Name = "Nome Social")]
        public string NomeSocial { get; set; }

        [Display(Name = "Data de nascimento")]
        [Column(TypeName = "datetime")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Display(Name = "CPF")]
        //levar para classe de validação
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 dígitos")]
        public string Cpf { get; set; }

        [Required]
        [StringLength(250)]
        public string Senha { get; set; }
    }
}