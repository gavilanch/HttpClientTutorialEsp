using Comun;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    public class PersonasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PersonasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Persona>>> Get()
        {
            Console.WriteLine("get");

            return await context.Personas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Persona persona)
        {
            context.Add(persona);
            await context.SaveChangesAsync();
            return persona.Id;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Persona persona)
        {
            Console.WriteLine("prueba");
            var personaExiste = await PersonaExiste(id);
            Console.WriteLine("personaExiste: " + personaExiste);

            if (!personaExiste)
            {
                return NotFound();
            }

            context.Update(persona);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var personaExiste = await PersonaExiste(id);

            if (!personaExiste)
            {
                return NotFound();
            }

            context.Remove(new Persona() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> PersonaExiste(int id)
        {
            return await context.Personas.AnyAsync(p => p.Id == id);
        }
    }
}
