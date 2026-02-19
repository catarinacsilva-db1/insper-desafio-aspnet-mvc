using CadastroUsuarios.Service.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                case ValidacaoException ex:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;
                case UnauthorizedAccessException ex:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = "Acesso não autorizado.";
                    break;
                case ArgumentNullException ex:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = "Dados inválidos enviados.";
                    break;
                case InvalidOperationException ex:
                    statusCode = (int)HttpStatusCode.Forbidden;
                    message = "Operação não permitida.";
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "Erro interno do servidor.";
                    break;
            }

            Trace.TraceError("Ocorreu uma exceção: {0}", filterContext.Exception);

            filterContext.HttpContext.Response.StatusCode = statusCode;

            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
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