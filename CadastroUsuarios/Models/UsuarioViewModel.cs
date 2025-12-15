using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadastroUsuarios.Models
{
    public class UsuarioViewModel {

        [Display(Name = "Status")]
        public bool Ativo { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome do usuário")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        [StringLength(100)]
        [Display(Name = "Nome Social")]
        public string NomeSocial { get; set; }

        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(250)]
        public string Senha { get; set; }
    }
}