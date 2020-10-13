using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Entity.Users;
using System.WebCloud.DTOModels.Users;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public UsersController(DbContextSystem context)
        {
            _context = context;
        }

        // GET: api/Users/GetUsers 
        [HttpGet("[action]")]
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var usuario = await _context.Users.Include(u => u.rol).ToListAsync();

            return usuario.Select(u => new UserDTO
            {
                idusuario = u.idusuario,
                idrol = u.idrol,
                rol = u.rol.nombre,
                nombre = u.nombre,
                tipo_documento = u.tipo_documento,
                num_documento = u.num_documento,
                direccion = u.direccion,
                telefono = u.telefono,
                email = u.email,
                password_hash = u.password_hash,
                condicion = u.condicion
            });
        }
        // POST: api/Users/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CreateUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if (await _context.Users.AnyAsync(u => u.email == email))
            {
                return BadRequest("El email ya existe");
            }

            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);

            User usuario = new User
            {
                idrol = model.idrol,
                nombre = model.nombre,
                tipo_documento = model.tipo_documento,
                num_documento = model.num_documento,
                direccion = model.direccion,
                telefono = model.telefono,
                email = model.email.ToLower(),
                password_hash = passwordHash,
                password_salt = passwordSalt,
                condicion = true
            };

            _context.Users.Add(usuario);
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
        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }


       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.idusuario)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.idusuario == id);
        }
    }
}
