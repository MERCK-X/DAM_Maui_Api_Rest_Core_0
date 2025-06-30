using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Filters
{
    public class Seguridad : IActionFilter
    {
        //Este metodo no se usa
        public void OnActionExecuted(ActionExecutedContext context){  }

        //Este OnAction se ejecutará antes de que se ingrese a cada metodo de la API
        public void OnActionExecuting(ActionExecutingContext context)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();

            //Leer el token que está enviando la API
            string token = root.GetValue<string>("token");
            //Leer el token que está enviando el cliente 
            string tokenCliente = context.HttpContext.Request.Headers["token"];

            if (token == null || tokenCliente != token)
            {
                context.Result = new RedirectResult("api/Error");
            }
        }
    }
}
