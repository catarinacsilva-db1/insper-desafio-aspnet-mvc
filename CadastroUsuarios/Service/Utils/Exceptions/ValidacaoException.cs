using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastroUsuarios.Service.Utils.Exceptions
{
    public class ValidacaoException : Exception
    {
        public ValidacaoException(string message) : base(message)
        {
        }
    }
}