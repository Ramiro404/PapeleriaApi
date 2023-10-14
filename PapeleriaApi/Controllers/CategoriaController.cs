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
	public class CategoriaController : ControllerBase
	{
		private ICategoriaRepositorio _categoriaRepositorio;

		public CategoriaController(ICategoriaRepositorio categoriaRepositorio)
		{
			_categoriaRepositorio = categoriaRepositorio;
		}
		[HttpGet]
		[ActionName(nameof(Listar))]
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
		public IActionResult Listar(
			[FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
		{
			try
			{
				var datos = _categoriaRepositorio.Listar(cantidad, salto);
				var respuesta = new RespuestaApiPlural<Categoria>
				{
					Cantidad = cantidad,
					Salto = salto,
					Datos = datos,
					Error = null
				};
				return Ok(respuesta);
			}
			catch (Exception ex)
			{
				var respuesta = new RespuestaApiPlural<Categoria>
				{
					Cantidad = cantidad,
					Salto = salto,
					Datos = null,
					Error = ex.Message
				};
				return StatusCode(500, respuesta);
			}
		}

		[HttpGet("{id}")]
		[ActionName(nameof(ObtenerPorId))]
		[Authorize(Roles = Constantes.ROL_ADMIN_O_VENDEDOR)]
		public ActionResult<Categoria> ObtenerPorId(int id)
		{
			try
			{
				var entidad = _categoriaRepositorio.ObtenerPorId(id);
				if (entidad == null)
				{
					return NotFound();
				}
				var respuesta = new RespuestaApiSingular<Categoria>
				{
					Datos = entidad,
					Error = null
				};
				return Ok(respuesta);
			}
			catch (Exception ex)
			{
				var respuesta = new RespuestaApiSingular<Categoria>
				{
					Datos = null,
					Error = ex.ToString()
				};
				return StatusCode(500, respuesta);
			}
		}

		[HttpPost]
		[ActionName(nameof(CrearAsync))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<ActionResult<Categoria>> CrearAsync(Categoria entidad)
		{
			try
			{
				await _categoriaRepositorio.GuardarAsync(entidad);
				var nuevaCategoria = CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
				return nuevaCategoria;
			}
			catch (Exception ex)
			{
				var respuesta = new RespuestaApiSingular<Categoria>
				{
					Datos = null,
					Error = ex.Message,
				};
				return StatusCode(500, respuesta);
			}
		}

		[HttpPut("{id}")]
		[ActionName(nameof(Actualizar))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<ActionResult> Actualizar(int id, Categoria entidad)
		{
			try
			{
				if (id != entidad.Id)
				{
					return BadRequest();
				}
				await _categoriaRepositorio.ActualizarAsync(entidad);
				return NoContent();
			}
			catch (Exception ex)
			{
				var respuesta = new RespuestaApiSingular<Categoria>
				{
					Datos = null,
					Error = ex.Message,
				};
				return StatusCode(500, respuesta);
			}

		}

		[HttpDelete("{id}")]
		[ActionName(nameof(Eliminar))]
		[Authorize(Roles = Constantes.ROL_ADMIN)]
		public async Task<IActionResult> Eliminar(int id)
		{
			try
			{
				var entidad = _categoriaRepositorio.ObtenerPorId(id);
				if (entidad == null) { return NotFound(); }
				await _categoriaRepositorio.EliminarAsync(entidad);
				return NoContent();
			}
			catch (Exception ex)
			{
				var respuesta = new RespuestaApiSingular<Categoria>
				{
					Datos = null,
					Error = ex.Message,
				};
				return StatusCode(500, respuesta);
			}
		}
	}
}
