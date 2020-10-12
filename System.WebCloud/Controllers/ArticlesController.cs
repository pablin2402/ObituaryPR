using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Database;
using System.Entity.WareHouse;
using System.WebCloud.DTOModels.WareHouse.Article;

namespace System.WebCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly DbContextSystem _context;

        public ArticlesController(DbContextSystem context)
        {
            _context = context;
        }

        // GET: api/Articles/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticleDTO>> List()
        {

            var article = await _context.Articles.Include(a => a.categoria).ToListAsync();

            return article.Select(a => new ArticleDTO
            {
                idarticulo = a.idarticulo,
                idcategoria = a.idcategoria,
                categoria = a.categoria.nombre,
                codigo = a.codigo,
                nombre = a.nombre,
                stock = a.stock,
                precio_venta = a.precio_venta,
                descripcion = a.descripcion,
                condicion = a.condicion
            });
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var articulo = await _context.Articles.Include(a => a.categoria).
               SingleOrDefaultAsync(a => a.idarticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(new ArticleDTO
            {
                idarticulo = articulo.idarticulo,
                idcategoria = articulo.idcategoria,
                categoria = articulo.categoria.nombre,
                codigo = articulo.codigo,
                nombre = articulo.nombre,
                descripcion = articulo.descripcion,
                stock = articulo.stock,
                precio_venta = articulo.precio_venta,
                condicion = articulo.condicion
            });
        }


        // PUT: api/Articles/PutArticle
        [HttpPut("[action]")]
        public async Task<IActionResult> PutArticle([FromBody] UploadDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idarticulo <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articles.FirstOrDefaultAsync(a => a.idarticulo == model.idarticulo);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.idcategoria = model.idcategoria;
            articulo.codigo = model.codigo;
            articulo.nombre = model.nombre;
            articulo.precio_venta = model.precio_venta;
            articulo.stock = model.stock;
            articulo.descripcion = model.descripcion;

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

        // PUT: api/Articles/PostArticle

        [HttpPost("[action]")]
        public async Task<ActionResult<Article>> PostArticle([FromBody] NewDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Article articulo = new Article
            {
                idcategoria = model.idcategoria,
                codigo = model.codigo,
                nombre = model.nombre,
                precio_venta = model.precio_venta,
                stock = model.stock,
                descripcion = model.descripcion,
                condicion = true
            };

            _context.Articles.Add(articulo);
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
        
        // PUT: api/Articulos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var articulo = await _context.Articles.FirstOrDefaultAsync(a => a.idarticulo == id);
            if (articulo == null)
            {
                return NotFound();
            }
            articulo.condicion = false;
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

        // PUT: api/Articulos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articles.FirstOrDefaultAsync(a => a.idarticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.condicion = true;

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


        private bool ArticuloExists(int id)
        {
            return _context.Articles.Any(e => e.idarticulo == id);
        }
    }
}
