using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Entity.Users;
using System.WebCloud.DTOModels.Users;
using System.WebCloud.DTOModels.Users.Rol;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolsController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public RolsController(DbContextSystem context)
        {
            _context = context;
        }

        // GET: api/Rols/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolDTO>> Listar()
        {
            var rol = await _context.Rols.ToListAsync();

            return rol.Select(r => new RolDTO
            {
                idrol = r.idrol,
                nombre = r.nombre,
                descripcion = r.descripcion,
                condicion = r.condicion
            });

        }
        // GET: api/Rols/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectRolDTO>> Select()
        {
            var rol = await _context.Rols.Where(r => r.condicion == true).ToListAsync();
            return rol.Select(r => new SelectRolDTO
            {
                idrol = r.idrol,
                nombre = r.nombre
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, Rol rol)
        {
            if (id != rol.idrol)
            {
                return BadRequest();
            }

            _context.Entry(rol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rols
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            _context.Rols.Add(rol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRol", new { id = rol.idrol }, rol);
        }

        // DELETE: api/Rols/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rol>> DeleteRol(int id)
        {
            var rol = await _context.Rols.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            _context.Rols.Remove(rol);
            await _context.SaveChangesAsync();

            return rol;
        }

        private bool RolExists(int id)
        {
            return _context.Rols.Any(e => e.idrol == id);
        }
    }
}
