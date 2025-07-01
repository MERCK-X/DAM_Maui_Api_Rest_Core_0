using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Web.Cliente.Clases;
using System.Net.Http;

namespace Web.Cliente.Controllers
{
    public class LoginController : Controller
    {
        private string urlbase;
        private string cadena;
        private readonly IHttpClientFactory _httpClientFactory;
        private string token;

        public LoginController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            urlbase = configuration["baseurl"];
            cadena = "hola te hace la cola";
            _httpClientFactory = httpClientFactory;
            token = configuration["token"];
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("iidusuario");
            return RedirectToAction("Index");
        }

        //Metodo de login
        public async Task<int> login(string nombreusuario, string contra)
        {
            int logueo = await ClientHttp.GetInt(_httpClientFactory, urlbase, "/api/Login/" + nombreusuario + "/" + contra);

            if(logueo != 0)
            {
                //Aqui es donde se inicia la sesion
                HttpContext.Session.SetInt32("iidusuario", logueo);
            }

            return logueo;
        }
    }
}
