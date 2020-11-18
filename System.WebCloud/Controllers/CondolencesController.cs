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

    }
}
