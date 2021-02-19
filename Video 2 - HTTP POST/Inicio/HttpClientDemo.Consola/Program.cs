using Comun;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

            }
        }
    }
}