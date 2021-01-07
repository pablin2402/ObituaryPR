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

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public SalesController(DbContextSystem context)
        {
            _context = context;
        }

        // GET: api/Sales/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SaleDTO>> Listar()
        {
            var venta = await _context.Sales
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .OrderByDescending(v => v.idventa)
                .Take(100)
                .ToListAsync();

            return venta.Select(v => new SaleDTO
            {
                idventa = v.idventa,
                idcliente = v.idcliente,
                cliente = v.persona.nombre,
                idusuario = v.idusuario,
                usuario = v.usuario.nombre,
                tipo_comprobante = v.tipo_comprobante,
                serie_comprobante = v.serie_comprobante,
                num_comprobante = v.num_comprobante,
                fecha_hora = v.fecha_hora,
                impuesto = v.impuesto,
                total = v.total,
                estado = v.estado
            });

        }


        // GET: api/Ventas/ListarFiltro/texto
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<SaleDTO>> ListarFiltro([FromRoute] string texto)
        {
            var venta = await _context.Sales
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .Where(v => v.num_comprobante.Contains(texto))
                .OrderByDescending(v => v.idventa)
                .ToListAsync();

            return venta.Select(v => new SaleDTO
            {
                idventa = v.idventa,
                idcliente = v.idcliente,
                cliente = v.persona.nombre,
                idusuario = v.idusuario,
                usuario = v.usuario.nombre,
                tipo_comprobante = v.tipo_comprobante,
                serie_comprobante = v.serie_comprobante,
                num_comprobante = v.num_comprobante,
                fecha_hora = v.fecha_hora,
                impuesto = v.impuesto,
                total = v.total,
                estado = v.estado
            });

        }



        // GET: api/Ventas/ListarDetalles
        [HttpGet("[action]/{idventa}")]
        public async Task<IEnumerable<DetailSaleDTO>> ListarDetalles([FromRoute] int idventa)
        {
            var detalle = await _context.DetailsSales
                .Include(a => a.articulo)
                .Where(d => d.idventa == idventa)
                .ToListAsync();

            return detalle.Select(d => new DetailSaleDTO
            {
                idarticulo = d.idarticulo,
                articulo = d.articulo.nombre,
                cantidad = d.cantidad,
                precio = d.precio,
                descuento = d.descuento
            });

        }

        // POST: api/Sales/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CreateSaleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            Sale venta = new Sale
            {
                idcliente = model.idcliente,
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
                _context.Sales.Add(venta);
                await _context.SaveChangesAsync();

                var id = venta.idventa;
                foreach (var det in model.detalles)
                {
                    SalesDetails detalle = new SalesDetails
                    {
                        idventa = id,
                        idarticulo = det.idarticulo,
                        cantidad = det.cantidad,
                        precio = det.precio,
                        descuento = det.descuento
                    };
                    _context.DetailsSales.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Sales/Anular/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var venta = await _context.Sales.FirstOrDefaultAsync(v => v.idventa == id);

            if (venta == null)
            {
                return NotFound();
            }

            venta.estado = "Anulado";

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
    }
}
