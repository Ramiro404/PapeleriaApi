using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Repositorios;

namespace PapeleriaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolController : ControllerBase
	{
		private IRolRepositorio _rolRepositorio;

		public RolController(IRolRepositorio rolRepositorio)
		{
			_rolRepositorio = rolRepositorio;
		}

		[HttpGet]
		[ActionName(nameof(Listar))]
		public IEnumerable<Rol> Listar([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
		{
			return _rolRepositorio.Listar(cantidad, salto);
		}

		[HttpGet("{id}")]
		[ActionName(nameof(ObtenerPorId))]
		public ActionResult<Rol> ObtenerPorId(int id)
		{
			var entidad = _rolRepositorio.ObtenerPorId(id);
			if (entidad == null)
			{
				return NotFound();
			}
			return entidad;
		}

		[HttpPost]
		[ActionName(nameof(CrearAsync))]
		public async Task<ActionResult<Rol>> CrearAsync(Rol entidad)
		{
			await _rolRepositorio.GuardarAsync(entidad);
			return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
		}

		[HttpPut("{id}")]
		[ActionName(nameof(Actualizar))]
		public async Task<ActionResult> Actualizar(int id, Rol entidad)
		{
			if (id != entidad.Id)
			{
				return BadRequest();
			}
			await _rolRepositorio.ActualizarAsync(entidad);
			return NoContent();
		}

		[HttpDelete("{id}")]
		[ActionName(nameof(Eliminar))]
		public async Task<IActionResult> Eliminar(int id)
		{
			var entidad = _rolRepositorio.ObtenerPorId(id);
			if (entidad == null) { return NotFound(); }
			await _rolRepositorio.EliminarAsync(entidad);
			return NoContent();
		}
	}
}
