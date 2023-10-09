using Microsoft.AspNetCore.Mvc;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Repositorios;

namespace PapeleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private IProductoRepositorio _productoRepository;
        public ProductoController(IProductoRepositorio productoRepository) { 
            _productoRepository = productoRepository; 
        }

        [HttpGet]
        [ActionName(nameof(ListarProductosAsync))]
        public IEnumerable<Producto> ListarProductosAsync([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
        {
            return _productoRepository.ListarProductos(cantidad, salto);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(ObtenerProductoPorId))]
        public ActionResult<Producto> ObtenerProductoPorId(int id)
        {
            var producto = _productoRepository.ObtenerProductoPorId(id);
            if(producto == null)
            {
                return NotFound();
            }
            return producto;
        }

        [HttpPost]
        [ActionName(nameof(CrearProductoAsync))]
        public async Task<ActionResult<Producto>> CrearProductoAsync(Producto producto)
        {
            await _productoRepository.GuardarProductoAsync(producto);
            return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = producto.Id}, producto);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(ActualizarProducto))]
        public async Task<ActionResult> ActualizarProducto(int id, Producto producto)
        {
            if(id != producto.Id)
            {
                return BadRequest();
            }
            await _productoRepository.ActualizarProductoAsync(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(EliminarProducto))]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = _productoRepository.ObtenerProductoPorId(id);
            if (producto == null) { return NotFound(); }
            await _productoRepository.EliminarProductoAsync(producto);
            return NoContent();
        }
    }
}
