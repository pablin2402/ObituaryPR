using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Entity.Users;
using System.WebCloud.DTOModels.Users;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeathPeoplesController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public DeathPeoplesController(DbContextSystem context)
        {
            _context = context;
        }

        // GET: api/DeathPeoples
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeathPeople>>> GetDeathPeoples()
        {
            return await _context.DeathPeoples.ToListAsync();
        }
        // GET: api/DeathPeoples/GetDeathPeople
        [HttpGet("[action]")]
        public async Task<IEnumerable<DeathPeopleDTO>> GetDeathPeople()
        {
            var mortuary = await _context.DeathPeoples.Include(a => a.usuario).ToListAsync();

            return mortuary.Select(a => new DeathPeopleDTO
            {
                 idmuerto = a.idmuerto,
                 idusuario = a.idusuario,
                 usuario = a.usuario.nombre,
                 nombre = a.nombre,
                 apellido = a.apellido,
                 fecha_nacimiento = a.fecha_nacimiento,
                 fecha_muerte = a.fecha_muerte,
                 lugar_nacimiento = a.lugar_nacimiento,
                 imagen1 = a.imagen1,
                 imagen2= a.imagen2,
                 imagen3= a.imagen3,
                 imagen4= a.imagen3,
                 imagen5= a.imagen4,
                 descripcion = a.descripcion,
                 musica = a.musica,
                 genero = a.genero,
                 designacion = a.designacion,
                 condicion = a.condicion

            });

        }

       
       
        // POST: api/DeathPeoples/PostDeath
        [HttpPost("[action]")]
        public async Task<IActionResult> PostDeath([FromBody] DeathPeopleAddDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DeathPeople deathpeople = new DeathPeople
            {
                idusuario = model.idusuario,
                nombre = model.nombre,
                apellido = model.apellido,
                fecha_nacimiento = model.fecha_nacimiento,
                fecha_muerte = model.fecha_muerte,
                lugar_nacimiento = model.lugar_nacimiento,
                imagen1 = model.imagen1,
                imagen2 = model.imagen2,
                imagen3 = model.imagen3,
                imagen4 = model.imagen4,
                imagen5 = model.imagen5,
                descripcion = model.descripcion,
                musica = model.musica,
                genero = model.genero,
                designacion = model.designacion,
                condicion = true
            };
            _context.DeathPeoples.Add(deathpeople);
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
