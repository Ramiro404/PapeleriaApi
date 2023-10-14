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
	[Authorize(Roles = Constantes.ROL_ADMIN)]
	public class UsuarioController : ControllerBase
    {
        private IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio vendedorRepositorio) {
            _usuarioRepositorio= vendedorRepositorio;
        }

        [HttpGet]
        [ActionName(nameof(Listar))]
        public ActionResult Listar([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
        {
            try
            {
                var datos = _usuarioRepositorio.Listar(cantidad,  salto);
				var respuesta = new RespuestaApiPlural<Usuario>
				{
					Error = null,
					Datos = datos,
					Cantidad = cantidad,
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
        public ActionResult<UsuarioDto> ObtenerPorId(int id)
        {
            try
            {
				var entidad = _usuarioRepositorio.ObtenerDtoPorId(id);
				if (entidad == null)
				{
					return NotFound();
				}
				var respuesta = new RespuestaApiSingular<UsuarioDto>
				{
					Error = null,
					Datos = entidad
				};
				return Ok(respuesta);
			}
			catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost]
        [ActionName(nameof(CrearAsync))]
        public async Task<ActionResult<Usuario>> CrearAsync(Usuario entidad)
        {
            try
            {
				await _usuarioRepositorio.GuardarAsync(entidad);
				return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
			}catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }

        [HttpPut("{id}")]
        [ActionName(nameof(Actualizar))]
        public async Task<ActionResult> Actualizar(int id, Usuario entidad)
        {
            try
            {
				if (id != entidad.Id)
				{
					return BadRequest();
				}
				await _usuarioRepositorio.ActualizarAsync(entidad);
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
				var entidad = _usuarioRepositorio.ObtenerPorId(id);
				if (entidad == null) { return NotFound(); }
				await _usuarioRepositorio.EliminarAsync(entidad);
				return NoContent();
			}catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

    }
}
