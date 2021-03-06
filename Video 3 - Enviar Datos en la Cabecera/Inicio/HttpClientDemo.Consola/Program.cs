using Comun;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClientDemo.Consola
{
    class Program
    {
        static string urlCuentas = "https://localhost:5001/api/cuentas";
        static UserInfo credenciales = new UserInfo() { Email = "felipe@hotmail.com", Password = "aA123456!" };

        static async Task Main(string[] args)
        {
            var urlPersonas = "https://localhost:5001/api/personas";
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            var urlEjemplo1 = "https://localhost:5001/WeatherForecast";

            using (var httpClient = new HttpClient())
            {
                // Ejemplo 1: Mandar una vez un valor por la cabecera de la petición HTTP - SendAsync
                // Ideal cuando no queremos afectar otras peticiones HTTP

                // Ejemplo 2: Mandar siempre un valor por la cabecera

                // Ejemplo 3: Mandar un JWT
            }
        }

        private async static Task CrearUsuario()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var respuesta = await httpClient.PostAsJsonAsync($"{urlCuentas}/crear", credenciales);
                    if (respuesta.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        respuesta.EnsureSuccessStatusCode();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}