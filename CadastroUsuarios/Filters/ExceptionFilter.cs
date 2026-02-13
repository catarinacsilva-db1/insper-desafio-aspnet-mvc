using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebGrease.Activities;

namespace CadastroUsuarios.Filters
{
    public class ExceptionFilter : HandleErrorAttribute
    {
        int statusCode;
        string message;

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            switch (filterContext.Exception)
            {
                case UnauthorizedAccessException ex:
                    statusCode = 401;
                    message = "Acesso não autorizado.";
                    break;
                case ArgumentNullException ex:
                    statusCode = 400;
                    message = "Dados inválidos enviados.";
                    break;
                case InvalidOperationException ex:
                    statusCode = 403;
                    message = "Operação não permitida.";
                    break;
                default:
                    statusCode = 500;
                    message = "Erro interno do servidor.";
                    break;
            }
            
            Console.WriteLine("Exception occurred: " + filterContext.Exception.Message);

            filterContext.Result = new ViewResult
            {
                ViewName = "Error",//TODO: alterar a view de erro para receber o status code e a mensagem
                ViewData = new ViewDataDictionary
                {
                    { "StatusCode", statusCode },
                    { "Message", message }
                }
            };


            filterContext.ExceptionHandled = true;
        }
    }
}