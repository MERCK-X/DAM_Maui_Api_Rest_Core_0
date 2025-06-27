using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Web.Cliente.Clases;

namespace Web.Cliente.Controllers
{
    public class TipoUsuarioController : Controller
    {
        private string urlbase;
        private readonly IHttpClientFactory _httpClientFactory;

        public TipoUsuarioController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            urlbase = configuration["baseurl"];
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<List<TipoUsuarioCLS>> listarTipoUsuario()
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //string cadena = await cliente.GetStringAsync("api/Persona/recuperarPersona/" + id);
            //PersonaCLS persona = JsonSerializer.Deserialize<PersonaCLS>(cadena);
            //return persona;
            List<TipoUsuarioCLS> lista = await ClientHttp.GetAll<TipoUsuarioCLS>(_httpClientFactory, urlbase, "/api/TipoUsuario");

            return lista;
        }
    }
}
