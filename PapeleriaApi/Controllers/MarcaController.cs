using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Repositorios;

namespace PapeleriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        public IMarcaRepositorio _marcaRepositorio;
        public MarcaController(IMarcaRepositorio marcaRepositorio) {
            _marcaRepositorio= marcaRepositorio;
        }

        [HttpGet]
        [ActionName(nameof(Listar))]
        public IEnumerable<Marca> Listar([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
        {
            return _marcaRepositorio.Listar(cantidad, salto);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(ObtenerPorId))]
        public ActionResult<Marca> ObtenerPorId(int id)
        {
            var entidad = _marcaRepositorio.ObtenerPorId(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return entidad;
        }

        [HttpPost]
        [ActionName(nameof(CrearAsync))]
        public async Task<ActionResult<Categoria>> CrearAsync(Marca entidad)
        {
            await _marcaRepositorio.GuardarAsync(entidad);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(Actualizar))]
        public async Task<ActionResult> Actualizar(int id, Marca entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest();
            }
            await _marcaRepositorio.ActualizarAsync(entidad);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(Eliminar))]
        public async Task<IActionResult> Eliminar(int id)
        {
            var entidad = _marcaRepositorio.ObtenerPorId(id);
            if (entidad == null) { return NotFound(); }
            await _marcaRepositorio.EliminarAsync(entidad);
            return NoContent();
        }
    }
}
