using CapaEntidad;
using System.Text.Json;

namespace Web.Cliente.Clases
{
    public class ClientHttp
    {
        //listar personas sin filtro
        public static async Task<List<T>> GetAll<T>(IHttpClientFactory _httpClientFactory, string urlbase, string rutaapi) 
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                cliente.BaseAddress = new Uri(urlbase);
                string cadena = await cliente.GetStringAsync(rutaapi);
                List<T> lista = JsonSerializer.Deserialize<List<T>>(cadena);
                return lista;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error: {ex.Message}");
                return new List<T>();
            }
                
        }

        //Metodo para listar personas con filtro
        public static async Task<T> Get<T>(IHttpClientFactory _httpClientFactory, string urlbase, string rutaapi)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                cliente.BaseAddress = new Uri(urlbase);
                string cadena = await cliente.GetStringAsync(rutaapi);
                T lista = JsonSerializer.Deserialize<T>(cadena);
                return lista;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error: {ex.Message}");
                return (T) Activator.CreateInstance(typeof(T));
            }

        }
    }
}
