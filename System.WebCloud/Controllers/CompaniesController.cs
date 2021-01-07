using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Entity.Users;
using System.WebCloud.DTOModels.Users.Company;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public CompaniesController(DbContextSystem context)
        {
            _context = context;
        }

        // GET: api/Companies/GetCompany
        [HttpGet("[action]")]
        public async Task<IEnumerable<AddCompanyDTO>> GetCompany()
        {
            var company = await _context.Companies.
                                        Include(a => a.usuario).
                                        Include(a => a.categoria).
                                        ToListAsync();

            return company.Select(a => new AddCompanyDTO
            {
                idempresa = a.idempresa,
                nombre = a.nombre,
                descripcion = a.descripcion,
                condicion = a.condicion,
                idusuario = a.idusuario,
                usuario = a.usuario.nombre,
                idcategoria = a.idcategoria,
                categoria = a.categoria.nombre
            });

        }
        // POST: api/Companies/PostCompanies
        [HttpPost("[action]")]
        public async Task<IActionResult> PostCompanies([FromBody] CreateCompanyDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            Company deathpeople = new Company
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true,
                idusuario = model.idusuario,
                idcategoria = model.idcategoria
            };
            _context.Companies.Add(deathpeople);
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

        // api/Companies/GetbyUser
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<AddCompanyDTO>> GetbyUser([FromRoute]int id)
        {
            var all = await _context.Companies.
                                Include(articulo => articulo.usuario).
                                Include(a => a.categoria).
                                Where(articulo => articulo.idusuario == id).
                                ToListAsync();


            return all.Select(a => new AddCompanyDTO
            {
                idempresa = a.idempresa,
                nombre = a.nombre,
                descripcion = a.descripcion,
                condicion = a.condicion,
                idusuario = a.idusuario,
                usuario = a.usuario.nombre,
                idcategoria = a.idcategoria,
                categoria = a.categoria.nombre
            });
        }
    }
}
