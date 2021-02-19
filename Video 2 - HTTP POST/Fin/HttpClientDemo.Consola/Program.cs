using Comun;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClientDemo.Consola
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://localhost:5001/api/personas";
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var httpClient = new HttpClient())
            {
                // Ej 1: PostAsJsonAsync
                //var persona1 = new Persona() { Nombre = "Felipe" };
                //var respuesta = await httpClient.PostAsJsonAsync(url, persona1);
                //if (respuesta.IsSuccessStatusCode)
                //{
                //    var cuerpo = await respuesta.Content.ReadAsStringAsync();
                //    Console.WriteLine("El id es " + cuerpo);
                //}

                // Ej 2: PostAsync
                //var persona2 = new Persona() { Nombre = "Claudia" };
                //var persona2Serializada = JsonSerializer.Serialize(persona2);
                //var content = new StringContent(persona2Serializada, Encoding.UTF8, "application/json");
                //var respuesta = await httpClient.PostAsync(url, content);

                // Ej 3: Validaciones
                var persona3 = new Persona() { Edad = -1, Email = "abc" };
                var respuesta = await httpClient.PostAsJsonAsync(url, persona3);

                if (respuesta.StatusCode == HttpStatusCode.BadRequest)
                {
                    var cuerpo = await respuesta.Content.ReadAsStringAsync();
                    var erroresDelAPI = Utilidades.ExtraerErroresDelWebAPI(cuerpo);

                    foreach (var campoErrores in erroresDelAPI)
                    {
                        Console.WriteLine($"-{campoErrores.Key}");
                        foreach (var error in campoErrores.Value)
                        {
                            Console.WriteLine($"  {error}");
                        }
                    }
                }

                var personas = JsonSerializer.Deserialize<List<Persona>>(
                    await httpClient.GetStringAsync(url), jsonSerializerOptions);
            }
        }
    }
}