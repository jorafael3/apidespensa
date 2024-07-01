using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DespensaAPI.Context;
using DespensaAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace DespensaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        [Authorize] // Requiere autenticación JWT para acceder
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()
        {
            var productos = await _context.GetProductos();
            return Ok(productos);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetProducto(int id)
        {
            var producto = await _context.GetProductoById(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Productos producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            var result = await _context.UpdateProducto(producto);
            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Productos>> PostProducto(Productos producto)
        {
            var result = await _context.AddProducto(producto);
            if (result == 0)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var result = await _context.DeleteProducto(id);
            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
