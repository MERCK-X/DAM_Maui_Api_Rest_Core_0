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
                return (T)Activator.CreateInstance(typeof(T));
            }

        }

        //Metodo para eliminar
        public static async Task<int> Delete(IHttpClientFactory _httpClientFactory, string urlbase, string rutaapi)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                cliente.BaseAddress = new Uri(urlbase);
                var response = await cliente.DeleteAsync(rutaapi);
                
                if(response.IsSuccessStatusCode)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    return int.Parse(cadena);
                }
                else
                {
                    return 0; 
                }
                
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //Metodo para guardar
        public static async Task<int> Post<T>(IHttpClientFactory _httpClientFactory, string urlbase, string rutaapi, T obj)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                cliente.BaseAddress = new Uri(urlbase);

                var response = await cliente.PostAsJsonAsync(rutaapi, obj);

                if (response.IsSuccessStatusCode)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    return int.Parse(cadena);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static async Task<List<T>> PostList<T>(IHttpClientFactory _httpClientFactory, string urlbase, string rutaapi, T obj)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                cliente.BaseAddress = new Uri(urlbase);

                var response = await cliente.PostAsJsonAsync(rutaapi, obj);

                if (response.IsSuccessStatusCode)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<T>>(cadena);
                }
                else
                {
                    return new List<T>();
                }

            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }
    }
}
