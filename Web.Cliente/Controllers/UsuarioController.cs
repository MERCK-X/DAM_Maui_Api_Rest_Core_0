using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using Web.Cliente.Clases;
using System.Text.Json;
using System.Collections.Generic;

namespace Web.Cliente.Controllers
{
    public class UsuarioController : Controller
    {
        private string urlbase;
        private readonly IHttpClientFactory _httpClientFactory;
        private string token;

        public UsuarioController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            urlbase = configuration["baseurl"];
            _httpClientFactory = httpClientFactory;
            token = configuration["token"];
        }

        public IActionResult Index()
        {
            return View();
        }

        //Traer los datos o Data como string
        //Metodo para listar personas sin filtro
        public async Task<List<UsuarioCLS>> listarUsuarios()
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //string cadena = await cliente.GetStringAsync("api/Persona");
            //List<PersonaCLS> lista = JsonSerializer.Deserialize<List<PersonaCLS>>(cadena);
            //return lista;
            //

            List<UsuarioCLS> lista = await ClientHttp.GetAll<UsuarioCLS>(_httpClientFactory, urlbase, "/api/Usuario", token); ;

            lista.Where(p => p.fotopersona == "").ToList().ForEach(p => p.fotopersona = "/img/nofoto.jpg");

            return lista;
        }

        public async Task<List<UsuarioCLS>> buscarUsuarios(UsuarioCLS oUsuarioCLS)
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //string cadena = await cliente.GetStringAsync("api/Persona");
            //List<PersonaCLS> lista = JsonSerializer.Deserialize<List<PersonaCLS>>(cadena);
            //return lista;
            //

            if (oUsuarioCLS.nombreusuario == null)
            {
                oUsuarioCLS.nombreusuario = "";
            }

            List<UsuarioCLS> lista = await ClientHttp.PostList<UsuarioCLS>(_httpClientFactory, urlbase, "/api/Usuario", oUsuarioCLS, token); ;

            lista.Where(p => p.fotopersona == "").ToList().ForEach(p => p.fotopersona = "/img/nofoto.jpg");

            return lista;

        }

        //Metodo para insertar usuario
        public async Task<int> guardarUsuario(UsuarioCLS oUsuarioCLS)
        {
            int res = await ClientHttp.Post<UsuarioCLS>(_httpClientFactory, urlbase, "/api/Usuario/guardarDatos", oUsuarioCLS, token);

            return res;
        }

        //Metodo recuperar usuario
        public async Task<UsuarioCLS> recuperarUsuario(int id)
        {
            return await ClientHttp.Get<UsuarioCLS>(_httpClientFactory, urlbase, $"/api/Usuario/" + id, token);
        }

        //Metodo para eliminar usuario
        public async Task<int> eliminarUsuario(int id)
        {
            return await ClientHttp.Delete(_httpClientFactory, urlbase, $"/api/Usuario/"+ id, token);
        }
    }
}
