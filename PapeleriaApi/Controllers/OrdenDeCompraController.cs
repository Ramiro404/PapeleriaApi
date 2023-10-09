using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Repositorios;
using PapeleriaApi.Servicios;
using System.Net;
using System.Security.Claims;

namespace PapeleriaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdenDeCompraController : ControllerBase
	{
		public IOrdenDeCompraRepositorio _ordenDeCompraRepositorio;
		private readonly IHttpContextAccessor httpContext;
		private readonly IServicioJwt servicioJwt;

		public OrdenDeCompraController(
			IOrdenDeCompraRepositorio crudRepositorio,
			IHttpContextAccessor httpContext,
			IServicioJwt servicioJwt)
		{
			_ordenDeCompraRepositorio = crudRepositorio;
			this.httpContext = httpContext;
			this.servicioJwt = servicioJwt;
		}

		[HttpGet]
		[ActionName(nameof(Listar))]
		public async Task<IActionResult> Listar([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
		{
			try
			{
				
				return Ok("Hapy");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				var resultado = new RespuestaApiList<OrdenDeCompra>
				{
					Error = "",
					Mensaje = ex.Message,
					Resultado = new List<OrdenDeCompra> { }
				};
				return StatusCode(500, resultado);
			}
		}

		[HttpGet("{id}")]
		[ActionName(nameof(ObtenerPorId))]
		public ActionResult<OrdenDeCompra> ObtenerPorId(int id)
		{
			var entidad = _ordenDeCompraRepositorio.ObtenerPorId(id);
			if (entidad == null)
			{
				return NotFound();
			}
			return entidad;
		}

		[HttpGet("folio/{folio}")]
		[ActionName(nameof(ObtenerPorId))]
		public IEnumerable<OrdenDeCompra> ObtenerPorId(string folio)
		{
			var entidad = _ordenDeCompraRepositorio.ListarPorFolio(folio);
			return entidad;
		}

		[HttpPost]
		[ActionName(nameof(CrearAsync))]
		public async Task<ActionResult<OrdenDeCompra>> CrearAsync(OrdenDeCompra entidad)
		{
			await _ordenDeCompraRepositorio.GuardarAsync(entidad);
			return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
		}

		[HttpPost("row")]
		[ActionName(nameof(CrearAsync))]
		public async Task<bool> CrearMultipleAsync(OrdenDeCompra[] entidad)
		{
			await _ordenDeCompraRepositorio.GuaradarMultiples(entidad);
			return true;
		}

		[HttpPut("{id}")]
		[ActionName(nameof(Actualizar))]
		public async Task<ActionResult> Actualizar(int id, OrdenDeCompra entidad)
		{
			if (id != entidad.Id)
			{
				return BadRequest();
			}
			await _ordenDeCompraRepositorio.ActualizarAsync(entidad);
			return NoContent();
		}

		[HttpDelete("{id}")]
		[ActionName(nameof(Eliminar))]
		public async Task<IActionResult> Eliminar(int id)
		{
			var entidad = _ordenDeCompraRepositorio.ObtenerPorId(id);
			if (entidad == null) { return NotFound(); }
			await _ordenDeCompraRepositorio.EliminarAsync(entidad);
			return NoContent();
		}
	}
}
