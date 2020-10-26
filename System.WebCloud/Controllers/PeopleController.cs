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
using System.WebCloud.DTOModels.Users;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly DbContextSystem _context;
        private readonly IConfiguration _config;
        public PeopleController(DbContextSystem context,IConfiguration config)
        {
            _config = config;
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
        // GET: api/People/ListarProveedores

        [HttpGet("[action]")]
        public async Task<IEnumerable<ProveedorDTO>> ListarProveedores()
        {
            var persona = await _context.Persons.Where(p => p.tipo_persona == "Proveedor").ToListAsync();

            return persona.Select(p => new ProveedorDTO
            {
                idpersona = p.idpersona,
                nombre = p.nombre,
                
            });

        }
        // GET: api/People/ListarProveedoresDatos

        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonDTO>> ListarProveedoresDatos()
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
        // PUT: api/Personas/Actualizar

        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] UploadPeopleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idpersona <= 0)
            {
                return BadRequest();
            }

            var persona = await _context.Persons.FirstOrDefaultAsync(p => p.idpersona == model.idpersona);

            if (persona == null)
            {
                return NotFound();
            }

            persona.tipo_persona = model.tipo_persona;
            persona.nombre = model.nombre;
            persona.tipo_documento = model.tipo_documento;
            persona.num_documento = model.num_documento;
            persona.direccion = model.direccion;
            persona.telefono = model.telefono;
            persona.email = model.email.ToLower();

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
        // PUT: api/People/Login

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var email = model.email.ToLower();

            var usuario = await _context.Users.Where(u => u.condicion == true).Include(u => u.rol).FirstOrDefaultAsync(u => u.email == email);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!VerificarPasswordHash(model.password, usuario.password_hash, usuario.password_salt))
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.idusuario.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, usuario.rol.nombre ),
                new Claim("idusuario", usuario.idusuario.ToString() ),
                new Claim("rol", usuario.rol.nombre ),
                new Claim("nombre", usuario.nombre )
            };

            return Ok(
                    new { token = GenerarToken(claims) }
                );

        }
        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }
        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(1),
              signingCredentials: creds,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
