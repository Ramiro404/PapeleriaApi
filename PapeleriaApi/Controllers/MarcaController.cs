using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
	public class MarcaController : ControllerBase
	{
		public IMarcaRepositorio _marcaRepositorio;
		public MarcaController(IMarcaRepositorio marcaRepositorio)
		{
			_marcaRepositorio = marcaRepositorio;
		}

		[HttpGet]
		[ActionName(nameof(Listar))]
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
		public ActionResult Listar([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
		{
			try
			{
				var datos = _marcaRepositorio.Listar(cantidad, salto);
				var respuesta = new RespuestaApiPlural<Marca>
				{
					Cantidad = cantidad,
					Datos = datos,
					Error = null,
					Salto = salto
				};
				return Ok(respuesta);
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}")]
		[ActionName(nameof(ObtenerPorId))]
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
		public ActionResult<Marca> ObtenerPorId(int id)
		{
			try
			{
				var entidad = _marcaRepositorio.ObtenerPorId(id);
				if (entidad == null)
				{
					return NotFound();
				}
				var respuesta = new RespuestaApiSingular<Marca>
				{
					Datos = entidad,
					Error = null
				};
				return Ok(respuesta);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		[ActionName(nameof(CrearAsync))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<ActionResult<Categoria>> CrearAsync(Marca entidad)
		{
			try
			{
				await _marcaRepositorio.GuardarAsync(entidad);
				return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

		}

		[HttpPut("{id}")]
		[ActionName(nameof(Actualizar))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<ActionResult> Actualizar(int id, Marca entidad)
		{
			try
			{
				if (id != entidad.Id)
				{
					return BadRequest();
				}
				await _marcaRepositorio.ActualizarAsync(entidad);
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		[ActionName(nameof(Eliminar))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<IActionResult> Eliminar(int id)
		{
			try
			{
				var entidad = _marcaRepositorio.ObtenerPorId(id);
				if (entidad == null) { return NotFound(); }
				await _marcaRepositorio.EliminarAsync(entidad);
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
