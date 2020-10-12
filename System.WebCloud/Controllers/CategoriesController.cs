using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Web.DTOModels.WareHouse.Category;
using System.Entity.WareHouse;
using System.WebCloud.DTOModels.WareHouse.Category;

namespace System.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public CategoriesController(DbContextSystem context)
        {
            _context = context;
        }

        // GET: api/Categories/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoryDTO>> List()
        {
            var categoria = await _context.Categories.ToListAsync();

            return categoria.Select(c => new CategoryDTO
            {
                idcategoria = c.idcategoria,
                nombre = c.nombre,
                descripcion = c.descripcion,
                condicion = c.condicion
            });

        }
        // GET: api/Categories/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectDTO>> Select()
        {
            var categoria = await _context.Categories.Where( c => c.condicion == true).ToListAsync();

            return categoria.Select(c => new SelectDTO
            {
                idcategoria = c.idcategoria,
                nombre = c.nombre
            });

        }
        // GET: api/Categories/Get/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {

            var categoria = await _context.Categories.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(new CategoryDTO
            {
                idcategoria = categoria.idcategoria,
                nombre = categoria.nombre,
                descripcion = categoria.descripcion,
                condicion = categoria.condicion
            });
        }

        // PUT: api/Categories/Put
        [HttpPut("[action]")]
        public async Task<IActionResult> Put([FromBody] CategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.idcategoria <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categories.FirstOrDefaultAsync(c => c.idcategoria == model.idcategoria);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.nombre = model.nombre;
            categoria.descripcion = model.descripcion;

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
        // POST: api/Categories/Post
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromBody] CategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category categoria = new Category
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true
            };
            _context.Categories.Add(categoria);
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
        // DELETE: api/Categories/Delete/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Categories.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(categoria);
        }
        // PUT: api/Categories/Deactivate/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var categoria = await _context.Categories.FirstOrDefaultAsync(c => c.idcategoria == id);
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
        // PUT: api/Categories/Activate/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var categoria = await _context.Categories.FirstOrDefaultAsync(c => c.idcategoria == id);
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