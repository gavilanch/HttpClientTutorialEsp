using Comun;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientDemo.API.Controllers
{
    [ApiController]
    [Route("api/personas")]
    public class PersonasController: ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PersonasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Persona>>> Get()
        {
            //return NotFound();
            //return new List<Persona>() { new Persona() { Id = 1, Nombre = "Felipe" }, new Persona() { Id = 2, Nombre = "Claudia" } };
            return await context.Personas.ToListAsync();
        }
    }
}
