using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Entity.Sales;
using System.WebCloud.DTOModels.Sales;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public PeopleController(DbContextSystem context)
        {
            _context = context;
        }




        // GET: api/People/ListarClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonDTO>> ListarClientes()
        {
            var persona = await _context.Persons.Where(p => p.tipo_persona == "Cliente").ToListAsync();

            return persona.Select(p => new PersonDTO
            {
                idpersona = p.idpersona,
                tipo_persona = p.tipo_persona,
                nombre = p.nombre,
                tipo_documento = p.tipo_documento,
                num_documento = p.num_documento,
                direccion = p.direccion,
                telefono = p.telefono,
                email = p.email
            });

        }
        // GET: api/Personas/ListarProveedores
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonDTO>> ListarProveedores()
        {
            var persona = await _context.Persons.Where(p => p.tipo_persona == "Proveedor").ToListAsync();

            return persona.Select(p => new PersonDTO
            {
                idpersona = p.idpersona,
                tipo_persona = p.tipo_persona,
                nombre = p.nombre,
                tipo_documento = p.tipo_documento,
                num_documento = p.num_documento,
                direccion = p.direccion,
                telefono = p.telefono,
                email = p.email
            });

        }



        // POST: api/Personas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CreatePersonDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if (await _context.Persons.AnyAsync(p => p.email == email))
            {
                return BadRequest("El email ya existe");
            }

            Person persona = new Person
            {
                tipo_persona = model.tipo_persona,
                nombre = model.nombre,
                tipo_documento = model.tipo_documento,
                num_documento = model.num_documento,
                direccion = model.direccion,
                telefono = model.telefono,
                email = model.email.ToLower()
            };

            _context.Persons.Add(persona);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
