using Microsoft.AspNetCore.Mvc;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Dtos;
using PapeleriaApi.Modelos.Repositorios;

namespace PapeleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio vendedorRepositorio) {
            _usuarioRepositorio= vendedorRepositorio;
        }

        [HttpGet]
        [ActionName(nameof(Listar))]
        public IEnumerable<Usuario> Listar([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int posicion) { 
            return _usuarioRepositorio.Listar(cantidad, posicion);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(ObtenerPorId))]
        public ActionResult<UsuarioDto> ObtenerPorId(int id)
        {
            var entidad = _usuarioRepositorio.ObtenerDtoPorId(id);
            if(entidad == null)
            {
                return NotFound();
            }
            return entidad;
        }

        [HttpPost]
        [ActionName(nameof(CrearAsync))]
        public async Task<ActionResult<Usuario>> CrearAsync(Usuario entidad)
        {
            await _usuarioRepositorio.GuardarAsync(entidad);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(Actualizar))]
        public async Task<ActionResult> Actualizar(int id, Usuario entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest();
            }
            await _usuarioRepositorio.ActualizarAsync(entidad);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(Eliminar))]
        public async Task<IActionResult> Eliminar(int id)
        {
            var entidad = _usuarioRepositorio.ObtenerPorId(id);
            if (entidad == null) { return NotFound(); }
            await _usuarioRepositorio.EliminarAsync(entidad);
            return NoContent();
        }

    }
}
