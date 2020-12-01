using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Entity.WareHouse;
using System.WebCloud.DTOModels.WareHouse.Mortuary;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MortuariesController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public MortuariesController(DbContextSystem context)
        {
            _context = context;
        }
        // GET: api/Mortuaries/GetMortuaries
        [HttpGet("[action]")]
        public async Task<IEnumerable<MortuaryDTO>> GetMortuaries()
        {
            var mortuary = await _context.Mortuaries.Include(a => a.categoria).ToListAsync();

            return mortuary.Select(a => new MortuaryDTO
            {
                idfuneraria = a.idfuneraria,
                idcategoria = a.idcategoria,
                categoria = a.categoria.nombre,
                codigo = a.codigo,
                nombre = a.nombre,
                telefono = a.telefono,
                descripcion = a.descripcion,
                servicios = a.servicios,
                imagen = a.imagen,
                imagen2 = a.imagen2,
                posicionx = a.posicionx,
                posiciony = a.posiciony,
                condicion = a.condicion,
                direccion = a.direccion
            });
        }
        // POST: api/Mortuaries/PostMortuary
        [HttpPost("[action]")]
        public async Task<IActionResult> PostMortuary([FromBody] MortuaryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mortuary mortuary = new Mortuary
            {
                idcategoria = 2,
                codigo = model.codigo,
                nombre = model.nombre,
                telefono = model.telefono,
                descripcion = model.descripcion,
                servicios = model.servicios,
                imagen = model.imagen,
                imagen2 = model.imagen2,
                posicionx = model.posicionx,
                posiciony = model.posiciony,
                condicion = true,
                direccion = model.direccion
            };
            _context.Mortuaries.Add(mortuary);
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

        // GET: api/Mortuaries/GetMortuariesLocation
        [HttpGet("[action]")]
        public async Task<IEnumerable<MortuaryLocationDTO>> GetMortuariesLocation()
        {
            var mortuary = await _context.Mortuaries.Include(a => a.categoria).ToListAsync();

            return mortuary.Select(a => new MortuaryLocationDTO
            {
               
                nombre = a.nombre,
                
                posicionx = a.posicionx,
                posiciony = a.posiciony,
                
            });
        }
        // GET: api/Mortuaries/GetById/
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> GetById([FromRoute]int id)
        {
            var articulo = await _context.Mortuaries.Include(a => a.categoria).
               SingleOrDefaultAsync(a => a.idfuneraria == id);

            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(new MortuaryDTO
            {
                idfuneraria = articulo.idfuneraria,
                idcategoria = articulo.idcategoria,
                categoria = articulo.categoria.nombre,
                codigo = articulo.codigo,
                nombre = articulo.nombre,
                telefono = articulo.telefono,
                descripcion = articulo.descripcion,
                servicios = articulo.servicios,
                imagen = articulo.imagen,
                imagen2 = articulo.imagen2,
                posicionx = articulo.posicionx,
                posiciony = articulo.posiciony,
                condicion = articulo.condicion,
                direccion = articulo.direccion
            });
        }


    }
}
