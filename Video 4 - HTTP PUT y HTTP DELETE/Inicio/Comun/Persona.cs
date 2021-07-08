using System;
using System.ComponentModel.DataAnnotations;

namespace Comun
{
    public class Persona
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
