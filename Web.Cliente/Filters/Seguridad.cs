using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography.Xml;

namespace Web.Cliente.Filters
{
    public class Seguridad : IActionFilter
    {
        public Seguridad() { }

        //No se usa este metodo
        public void OnActionExecuted(ActionExecutedContext context){ }

        //Usaremos este OnAction 
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetInt32("iidusuario");
            if (session == null)
            {
                context.Result = new RedirectResult("/Login");
            }
            else
            {
                //Si existe la session, continuamos con la peticion
                context.HttpContext.Response.StatusCode = 200;
            }

        }
    }
}
