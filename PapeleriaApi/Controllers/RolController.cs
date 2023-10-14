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
	[Authorize(Roles = Constantes.ROL_ADMIN)]
	public class RolController : ControllerBase
	{
		private IRolRepositorio _rolRepositorio;

		public RolController(IRolRepositorio rolRepositorio)
		{
			_rolRepositorio = rolRepositorio;
		}

		[HttpGet]
		[ActionName(nameof(Listar))]
		public ActionResult Listar([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
		{
			try
			{
				var datos = _rolRepositorio.Listar(cantidad, salto);
				var respuesta = new RespuestaApiPlural<Rol>
				{
					Error = null,
					Datos = datos,
					Cantidad = cantidad,
					Salto = salto
				};
				return Ok(respuesta);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}")]
		[ActionName(nameof(ObtenerPorId))]
		public ActionResult<Rol> ObtenerPorId(int id)
		{
			try
			{
				var entidad = _rolRepositorio.ObtenerPorId(id);
				if (entidad == null)
				{
					return NotFound();
				}
				var respuesta = new RespuestaApiSingular<Rol>
				{
					Error = null,
					Datos = entidad
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
		public async Task<ActionResult<Rol>> CrearAsync(Rol entidad)
		{
			try
			{
				await _rolRepositorio.GuardarAsync(entidad);
				return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
			}catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}")]
		[ActionName(nameof(Actualizar))]
		public async Task<ActionResult> Actualizar(int id, Rol entidad)
		{
			try
			{
				if (id != entidad.Id)
				{
					return BadRequest();
				}
				await _rolRepositorio.ActualizarAsync(entidad);
				return NoContent();
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		[ActionName(nameof(Eliminar))]
		public async Task<IActionResult> Eliminar(int id)
		{
			try
			{
				var entidad = _rolRepositorio.ObtenerPorId(id);
				if (entidad == null) { return NotFound(); }
				await _rolRepositorio.EliminarAsync(entidad);
				return NoContent();
			}catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
