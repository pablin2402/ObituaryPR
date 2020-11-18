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
                posicionx= a.posicionx,
                posiciony=a.posiciony,
                condicion = a.condicion,
                direccion = a.direccion
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
