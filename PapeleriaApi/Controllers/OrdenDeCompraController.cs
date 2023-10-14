using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Dtos;
using PapeleriaApi.Modelos.Repositorios;
using PapeleriaApi.Servicios;
using PapeleriaApi.Utilidades;
using System.Data;
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
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
		public async Task<IActionResult> Listar([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
		{
			try
			{
				var respuesta = _ordenDeCompraRepositorio.Listar(cantidad, salto);
				return Ok(respuesta);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.ToString());
			}
		}

		[HttpGet("{id}")]
		[ActionName(nameof(ObtenerPorId))]
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
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
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
		public ActionResult<OrdenDeCompraDto> ObtenerPorFolio(string folio)
		{
			try
			{
				var entidad = _ordenDeCompraRepositorio.ListarPorFolio(folio);
				return Ok(entidad);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.ToString());
			}

		}

		[HttpPost]
		[ActionName(nameof(CrearAsync))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<ActionResult<OrdenDeCompra>> CrearAsync(OrdenDeCompra entidad)
		{
			await _ordenDeCompraRepositorio.GuardarAsync(entidad);
			return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
		}

		[HttpPost("multiple")]
		[ActionName(nameof(CrearAsync))]
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
		public async Task<IActionResult> CrearMultipleAsync(OrdenDeCompra[] entidad)
		{
			try
			{
				var resultado = await _ordenDeCompraRepositorio.GuardarMultiples(entidad);

				return StatusCode(201, resultado);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

		}

		[HttpPut("{id}")]
		[ActionName(nameof(Actualizar))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
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
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<IActionResult> Eliminar(int id)
		{
			var entidad = _ordenDeCompraRepositorio.ObtenerPorId(id);
			if (entidad == null) { return NotFound(); }
			await _ordenDeCompraRepositorio.EliminarAsync(entidad);
			return NoContent();
		}
	}
}
