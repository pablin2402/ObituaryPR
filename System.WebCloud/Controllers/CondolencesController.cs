using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Entity.Users;
using System.WebCloud.DTOModels.Users.Condolence;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondolencesController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public CondolencesController(DbContextSystem context)
        {
            _context = context;
        }

        // GET: api/Condolences/GetCondolence
        [HttpGet("[action]")]
        public async Task<IEnumerable<AddCondolenceDTO>> GetCondolence()
        {
            var mortuary = await _context.Condolences.
                                        Include(a => a.usuario).
                                        Include(a => a.fallecido).
                                        ToListAsync();

            return mortuary.Select(a => new AddCondolenceDTO
            {
               idcondolencia = a.idcondolencia,
               idusuario = a.idusuario,
               usuario = a.usuario.nombre,
               idmuerto = a.idmuerto,
               fallecido= a.fallecido.nombre,
               titulo = a.titulo,
               mensaje = a.mensaje,
               fecha= a.fecha,
               condicion= a.condicion



             });

        }
        // api/Condolences/GetbyUser
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<GetCondolenceByUserDTO>> GetbyUser([FromRoute]int id)
        {
            var all = await _context.Condolences.
                                Include(articulo => articulo.usuario).
                                Include(articulo => articulo.fallecido).
                                Where(articulo => articulo.idusuario == id).
                                ToListAsync();


            return all.Select(articulo => new GetCondolenceByUserDTO
            {

                idusuario = articulo.idusuario,
                idcondolencia = articulo.idcondolencia,
                fallecido = articulo.fallecido.nombre,
                titulo = articulo.titulo,
                mensaje = articulo.mensaje,
                fecha = articulo.fecha,
                condicion = articulo.condicion
            });
        }
        // api/Condolences/GetbyId
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<GetByIdDTO>> GetbyId([FromRoute]int id)
        {
            var all = await _context.Condolences.
                                Include(articulo => articulo.usuario).
                                Include(articulo => articulo.fallecido).
                                Where(articulo => articulo.idmuerto == id).
                                ToListAsync();

       
            return all.Select(articulo => new GetByIdDTO
            {
                idcondolencia = articulo.idcondolencia,
                usuario = articulo.usuario.nombre,
                fallecido = articulo.fallecido.nombre,
                titulo = articulo.titulo,
                mensaje = articulo.mensaje,
                fecha = articulo.fecha,
                condicion = articulo.condicion
            });
        }
        // POST: api/Condolences/PostCondolence
        [HttpPost("[action]")]
        public async Task<IActionResult> PostCondolence([FromBody] CreateCondolenceDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            Condolence deathpeople = new Condolence
            {
                idusuario = model.idusuario,
                idmuerto = model.idmuerto,
                titulo = model.titulo,
                mensaje = model.mensaje,
                fecha = fechaHora,
                condicion = true
            };
            _context.Condolences.Add(deathpeople);
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


        // PUT: api/Condolences/Deactivate/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var categoria = await _context.Condolences.FirstOrDefaultAsync(c => c.idcondolencia == id);
            if (categoria == null)
            {
                return NotFound();
            }
            categoria.condicion = false;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }
            return Ok();
        }
        // PUT: api/Condolences/Activate/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var categoria = await _context.Condolences.FirstOrDefaultAsync(c => c.idcondolencia == id);
            if (categoria == null)
            {
                return NotFound();
            }
            categoria.condicion = true;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
