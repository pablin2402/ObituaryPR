using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Entity.WareHouse;
using System.WebCloud.DTOModels.WareHouse.Entry;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public EntriesController(DbContextSystem context)
        {
            _context = context;
        }

        // GET: api/Entries/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<EntryDTO>> Listar()
        {
            var ingreso = await _context.Entries
                .Include(i => i.usuario)
                .Include(i => i.persona)
                .OrderByDescending(i => i.idingreso)
                .Take(100)
                .ToListAsync();

            return ingreso.Select(i => new EntryDTO
            {
                idingreso = i.idingreso,
                idproveedor = i.idproveedor,
                proveedor = i.persona.nombre,
                idusuario = i.idusuario,
                usuario = i.usuario.nombre,
                tipo_comprobante = i.tipo_comprobante,
                serie_comprobante = i.serie_comprobante,
                num_comprobante = i.num_comprobante,
                fecha_hora = i.fecha_hora,
                impuesto = i.impuesto,
                total = i.total,
                estado = i.estado
            });

        }
        // GET: api/Entries/ListarDetalles
        [HttpGet("[action]/{idingreso}")]
        public async Task<IEnumerable<DetailEntryDTO>> ListarDetalles([FromRoute] int idingreso)
        {
            var detalle = await _context.IncomeDetails
                .Include(a => a.articulo)
                .Where(d => d.iddetalle_ingreso == idingreso)
                .ToListAsync();

            return detalle.Select(d => new DetailEntryDTO
            {
                idarticulo = d.idarticulo,
                articulo = d.articulo.nombre,
                cantidad = d.cantidad,
                precio = d.precio
            });

        }
        // PUT: api/Entries/Anular/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var ingreso = await _context.Entries.FirstOrDefaultAsync(c => c.idingreso == id);

            if (ingreso == null)
            {
                return NotFound();
            }

            ingreso.estado = "Anulado";

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
        // POST: api/Entries/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CreateEntryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            Entry ingreso = new Entry
            {
                idproveedor = model.idproveedor,
                idusuario = model.idusuario,
                tipo_comprobante = model.tipo_comprobante,
                serie_comprobante = model.serie_comprobante,
                num_comprobante = model.num_comprobante,
                fecha_hora = fechaHora,
                impuesto = model.impuesto,
                total = model.total,
                estado = "Aceptado"
            };


            try
            {
                _context.Entries.Add(ingreso);
                await _context.SaveChangesAsync();

                var id = ingreso.idingreso;
                foreach (var det in model.detalles)
                {
                    IncomeDetail detalle = new IncomeDetail
                    {   
                        idingreso = id,
                        idarticulo = det.idarticulo,
                        cantidad = det.cantidad,
                        precio = det.precio,
                        nombre = det.nombre,
                        direccion = det.direccion,
                        telefono = det.telefono
                    };
                    _context.IncomeDetails.Add(detalle);
                }
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
