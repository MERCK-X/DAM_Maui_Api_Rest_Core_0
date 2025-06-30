using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Web.Cliente.Clases;

namespace Web.Cliente.Controllers
{
    public class PersonaController : Controller
    {
        private string urlbase;
        private string cadena;
        private readonly IHttpClientFactory _httpClientFactory;
        private string token;

        public PersonaController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
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


        //Traer los datos o Data como string
        //Metodo para listar personas sin filtro
        public async Task<List<PersonaCLS>> listarPersonas()
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //string cadena = await cliente.GetStringAsync("api/Persona");
            //List<PersonaCLS> lista = JsonSerializer.Deserialize<List<PersonaCLS>>(cadena);
            //return lista;
            //

            List<PersonaCLS> lista = await ClientHttp.GetAll<PersonaCLS>(_httpClientFactory, urlbase, "/api/Persona", token); ;

            lista.Where(p => p.fotocadena == "").ToList().ForEach(p => p.fotocadena = "/img/nofoto.jpg");

            return lista;
        }

        //Metodo para listar personas con filtro
        public async Task<List<PersonaCLS>> filtrarPersonas(string nombrecompleto)
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //string cadena = await cliente.GetStringAsync("api/Persona/"+nombrecompleto);
            //List<PersonaCLS> lista = JsonSerializer.Deserialize<List<PersonaCLS>>(cadena);
            //return lista;
            if (nombrecompleto != null)
            {
                List<PersonaCLS> lista = await ClientHttp.GetAll<PersonaCLS>(_httpClientFactory, urlbase, "/api/Persona/" + nombrecompleto, token);

                lista.Where(p => p.fotocadena == "").ToList().ForEach(p => p.fotocadena = "/img/nofoto.jpg");

                return lista;

            }
            else
            {
                return await listarPersonas();
            }
        }

        //Metodo para recuperar una persona por su id
        public async Task<PersonaCLS> recuperarPersona(int id)
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //string cadena = await cliente.GetStringAsync("api/Persona/recuperarPersona/" + id);
            //PersonaCLS persona = JsonSerializer.Deserialize<PersonaCLS>(cadena);
            //return persona;
            return await ClientHttp.Get<PersonaCLS>(_httpClientFactory, urlbase, "/api/Persona/recuperarPersona/" + id, token);
        }

        //Metodo para eliminar una persona por su id
        public async Task<int> eliminarPersona(int id)
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //var response = await cliente.DeleteAsync("api/Persona/eliminarPersona/" + id);
            //if(response.IsSuccessStatusCode)
            //{
            //    string cadena = await response.Content.ReadAsStringAsync();
            //    return int.Parse(cadena);
            //}
            //return 0;
            return await ClientHttp.Delete(_httpClientFactory, urlbase, "/api/Persona/" + id, token);
        }

        //Metodo para guardar una persona
        public async Task<int> guardarPersona(PersonaCLS oPersonaCLS, IFormFile fotoenviar)
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //var response = await cliente.DeleteAsync("api/Persona/eliminarPersona/" + id);
            //if(response.IsSuccessStatusCode)
            //{
            //    string cadena = await response.Content.ReadAsStringAsync();
            //    return int.Parse(cadena);
            //}
            //return 0;
            byte[] buffer = new byte[0];
            string nombrefoto = "";

            if (fotoenviar != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fotoenviar.CopyTo(ms);
                    
                    nombrefoto = fotoenviar.FileName;

                    buffer = ms.ToArray();
                }
            }

            oPersonaCLS.nombrearchivo = nombrefoto;
            oPersonaCLS.archivo = buffer;

            return await ClientHttp.Post(_httpClientFactory, urlbase, "/api/Persona/" , oPersonaCLS, token);
        }

        //Metodo para recuperar una persona sin usuario por su id
        public async Task<List<PersonaCLS>> listarPersonaSinUsuario()
        {
            //var cliente = _httpClientFactory.CreateClient();
            //cliente.BaseAddress = new Uri(urlbase);
            //string cadena = await cliente.GetStringAsync("api/Persona/recuperarPersona/" + id);
            //PersonaCLS persona = JsonSerializer.Deserialize<PersonaCLS>(cadena);
            //return persona;
            List<PersonaCLS> lista = await ClientHttp.GetAll<PersonaCLS>(_httpClientFactory, urlbase, "/api/Persona/listarPersonaSinUsuario", token);

            return lista;
        }
    }
}

