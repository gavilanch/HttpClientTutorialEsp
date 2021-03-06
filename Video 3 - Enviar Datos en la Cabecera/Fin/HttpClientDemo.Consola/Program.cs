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

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, urlEjemplo1))
                {
                    requestMessage.Headers.Add("cantidadElementos", "15");
                    var respuestaEjemplo1 = await httpClient.SendAsync(requestMessage);
                    var contenidoString = await respuestaEjemplo1.Content.ReadAsStringAsync();
                    var climasDatos = JsonSerializer.Deserialize<List<WeatherForecast>>(contenidoString,
                        jsonSerializerOptions);
                    Console.WriteLine($"Cantidad de climas #1: {climasDatos.Count}");
                }

                var climasDatos2 = await httpClient.GetFromJsonAsync<List<WeatherForecast>>(urlEjemplo1,
                    jsonSerializerOptions);
                Console.WriteLine($"Cantidad de climas #2: {climasDatos2.Count}");

                // Ejemplo 2: Mandar siempre un valor por la cabecera
                httpClient.DefaultRequestHeaders.Add("cantidadElementos", "30");
                var climasDatos3 = await httpClient.GetFromJsonAsync<List<WeatherForecast>>(urlEjemplo1,
                    jsonSerializerOptions);
                Console.WriteLine($"Cantidad de climas #3: {climasDatos3.Count}");

                var climasDatos4 = await httpClient.GetFromJsonAsync<List<WeatherForecast>>(urlEjemplo1,
                   jsonSerializerOptions);
                Console.WriteLine($"Cantidad de climas #4: {climasDatos4.Count}");

                // Ejemplo 3: Mandar un JWT
                await CrearUsuario();
                var httpRespuestaToken = await httpClient.PostAsJsonAsync($"{urlCuentas}/login", credenciales);
                var respuestaToken = JsonSerializer.Deserialize<UserToken>(await
                    httpRespuestaToken.Content.ReadAsStringAsync(), jsonSerializerOptions);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
                    respuestaToken.Token);
                var respuesta = await httpClient.PostAsJsonAsync(urlPersonas, new Persona() { Nombre = "Felipe" });
                respuesta.EnsureSuccessStatusCode();
                Console.WriteLine("Persona creada de manera exitosa");

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