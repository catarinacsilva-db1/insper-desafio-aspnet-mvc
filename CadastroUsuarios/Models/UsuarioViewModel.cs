using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadastroUsuarios.Models
{
    public class UsuarioViewModel {

        [Display(Name = "Nome do Usuario")]
        public string Nome { get; set; }
    }
}