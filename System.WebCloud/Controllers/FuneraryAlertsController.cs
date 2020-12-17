using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Entity.WareHouse;
using System.WebCloud.DTOModels.WareHouse.MortuaryAlert;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuneraryAlertsController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public FuneraryAlertsController(DbContextSystem context)
        {
            _context = context;
        }

        // GET: api/FuneraryAlerts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuneraryAlert>>> GetFuneraryAlert()
        {
            return await _context.FuneraryAlert.ToListAsync();
        }

        // api/FuneraryAlerts/GetbyUser
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<MortuaryAlertDTO>> GetbyUser([FromRoute]int id)
        {
            var all = await _context.FuneraryAlert.
                                Include(a => a.empresa).
                                Where(a => a.idempresa == id).
                                ToListAsync();


            return all.Select(a => new MortuaryAlertDTO
            {
                idempresa = a.idempresa,
                nombre = a.nombre,
                apellido = a.apellido,
                correo = a.correo,
                telefono = a.telefono,
                mensaje = a.mensaje,
                empresa = a.empresa.nombre,
                fecha = a.fecha
            });
        }
        // POST: api/FuneraryAlerts/PostAlert
        [HttpPost("[action]")]
        public async Task<IActionResult> PostAlert([FromBody] NewAlertDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            FuneraryAlert articulo = new FuneraryAlert
            {
                nombre = model.nombre,
                apellido = model.apellido,
                correo = model.correo,
                telefono = model.telefono,
                mensaje= model.mensaje,
                idempresa = model.idempresa,
                fecha = fechaHora

            };

            _context.FuneraryAlert.Add(articulo);
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
