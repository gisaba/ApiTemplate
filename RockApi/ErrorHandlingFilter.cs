using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace RockApi
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            Log.Debug("Errore Insert : {@classe} {@message}", this.ToString(), exception.Message);


            context.ExceptionHandled = true; //optional 
        }
    }
}

