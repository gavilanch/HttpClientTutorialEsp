using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClientDemo.Consola
{
    public class Utilidades
    {
        public static Dictionary<string, List<string>> ExtraerErroresDelWebAPI(string json)
        {
            var respuesta = new Dictionary<string, List<string>>();

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            var errorsJsonElement = jsonElement.GetProperty("errors");
            foreach (var campoConErrores in errorsJsonElement.EnumerateObject())
            {
                var campo = campoConErrores.Name;
                var errores = new List<string>();
                foreach (var errorKind in campoConErrores.Value.EnumerateArray())
                {
                    var error = errorKind.GetString();
                    errores.Add(error);
                }
                respuesta.Add(campo, errores);
            }

            return respuesta;
        }
    }
}
