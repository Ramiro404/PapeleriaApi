using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Dtos;
using PapeleriaApi.Modelos.Repositorios;
using PapeleriaApi.Utilidades;
using System.Data;

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
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
		public ActionResult ListarProductosAsync([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
        {
            try
            {
                var datos = _productoRepository.ListarProductos(cantidad, salto);
                var respuesta = new RespuestaApiPlural<Producto>
                {
                    Cantidad = cantidad,
                    Datos = datos,
                    Error = null,
                    Salto = salto
                };
                return Ok(respuesta);
			}catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ActionName(nameof(ObtenerProductoPorId))]
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
		public ActionResult<Producto> ObtenerProductoPorId(int id)
        {
            try
            {
				var producto = _productoRepository.ObtenerProductoPorId(id);
				if (producto == null)
				{
					return NotFound();
				}
                var respuesta = new RespuestaApiSingular<Producto>
                {
                    Datos = producto,
                    Error = null,
                };
				return Ok(producto);
			}catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ActionName(nameof(CrearProductoAsync))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<ActionResult<Producto>> CrearProductoAsync(Producto producto)
        {
            try
            {
				await _productoRepository.GuardarProductoAsync(producto);
				return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = producto.Id }, producto);
			}
			catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ActionName(nameof(ActualizarProducto))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<ActionResult> ActualizarProducto(int id, Producto producto)
        {
            try
            {
				if (id != producto.Id)
				{
					return BadRequest();
				}
				await _productoRepository.ActualizarProductoAsync(producto);
				return NoContent();
			}catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(EliminarProducto))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = _productoRepository.ObtenerProductoPorId(id);
            if (producto == null) { return NotFound(); }
            await _productoRepository.EliminarProductoAsync(producto);
            return NoContent();
        }
    }
}
