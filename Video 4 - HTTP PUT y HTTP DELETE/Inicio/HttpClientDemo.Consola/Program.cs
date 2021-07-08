using Comun;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HttpClientDemo.Consola
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var urlPersonas = "https://localhost:5001/api/personas";

            using (var httpClient = new HttpClient())
            {
                var persona = new Persona() { Nombre = "Felipe Gavilán" };

                // Creando la persona
                var responseMessage = await httpClient.PostAsJsonAsync(urlPersonas, persona);
                responseMessage.EnsureSuccessStatusCode();
                var content = await responseMessage.Content.ReadAsStringAsync();
                var idPersona = int.Parse(content);

                // Ejemplo 1: Hacer un HTTP PUT
            }

            Console.Read();
        }
    }
}