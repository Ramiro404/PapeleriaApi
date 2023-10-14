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
	public class ClienteController : ControllerBase
	{
		private IClienteRepositorio _clienteRepositorio;
		public ClienteController(IClienteRepositorio clienteRepositorio)
		{
			_clienteRepositorio = clienteRepositorio;
		}

		[HttpGet]
		[ActionName(nameof(Listar))]
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
		public ActionResult Listar([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
		{
			try
			{
				var datos = _clienteRepositorio.Listar(cantidad, salto);
				var respuesta = new RespuestaApiPlural<Cliente>
				{
					Cantidad = cantidad,
					Datos = datos,
					Error = null,
					Salto = salto
				};
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
		public ActionResult ObtenerPorId(int id)
		{
			try
			{
				var entidad = _clienteRepositorio.ObtenerPorId(id);
				if (entidad == null)
				{
					return NotFound();
				}
				var respuesta = new RespuestaApiSingular<Cliente>
				{
					Datos = entidad,
					Error = null
				};
				return Ok(respuesta);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.ToString());
			}

		}

		[HttpPost]
		[ActionName(nameof(CrearAsync))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<ActionResult<Cliente>> CrearAsync(Cliente entidad)
		{
			try
			{
				await _clienteRepositorio.GuardarAsync(entidad);
				return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
			}catch(Exception ex)
			{
				return StatusCode(500, ex.ToString());
			}
		}

		[HttpPut("{id}")]
		[ActionName(nameof(Actualizar))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<ActionResult> Actualizar(int id, Cliente entidad)
		{
			try
			{
				if (id != entidad.Id)
				{
					return BadRequest();
				}
				await _clienteRepositorio.ActualizarAsync(entidad);
				return NoContent();
			}
			catch(Exception ex)
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
				var entidad = _clienteRepositorio.ObtenerPorId(id);
				if (entidad == null) { return NotFound(); }
				await _clienteRepositorio.EliminarAsync(entidad);
				return NoContent();
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
