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
            var urlPersonas = "https://localhost:44301/api/personas";

            using (var httpClient = new HttpClient())
            {
                var persona = new Persona() { Nombre = "Felipe Gavilán" };

                // Creando la persona
                var responseMessage = await httpClient.PostAsJsonAsync(urlPersonas, persona);
                responseMessage.EnsureSuccessStatusCode();
                var content = await responseMessage.Content.ReadAsStringAsync();
                var idPersona = int.Parse(content);

                // Ejemplo 1: Hacer un HTTP PUT
                persona.Id = idPersona;
                persona.Nombre = "Actualizado";
                await httpClient.PutAsJsonAsync($"{urlPersonas}/{idPersona}", persona);

                var personas = await httpClient.GetFromJsonAsync<List<Persona>>(urlPersonas);

                foreach (var p in personas)
                {
                    Console.WriteLine($"Id: {p.Id} - Nombre: {p.Nombre}");
                }

                // Ejemplo 2: Hacer un HTTP DELETE

                await httpClient.DeleteAsync($"{urlPersonas}/{idPersona}");
                var personas2 = await httpClient.GetFromJsonAsync<List<Persona>>(urlPersonas);

                if (personas2.Count == 0)
                {
                    Console.WriteLine("No hay registros en la tabla de personas");
                }
            }

            Console.Read();
        }
    }
}