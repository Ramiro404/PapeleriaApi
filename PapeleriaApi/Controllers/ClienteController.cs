using Microsoft.AspNetCore.Mvc;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Repositorios;

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
        public IEnumerable<Cliente> Listar([FromQuery(Name = "cantidad")] int cantidad,
			[FromQuery(Name = "salto")] int salto)
        {
            return _clienteRepositorio.Listar(cantidad, salto);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(ObtenerPorId))]
        public ActionResult<Cliente> ObtenerPorId(int id)
        {
            var entidad = _clienteRepositorio.ObtenerPorId(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return entidad;
        }

        [HttpPost]
        [ActionName(nameof(CrearAsync))]
        public async Task<ActionResult<Cliente>> CrearAsync(Cliente entidad)
        {
            await _clienteRepositorio.GuardarAsync(entidad);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(Actualizar))]
        public async Task<ActionResult> Actualizar(int id, Cliente entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest();
            }
            await _clienteRepositorio.ActualizarAsync(entidad);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(Eliminar))]
        public async Task<IActionResult> Eliminar(int id)
        {
            var entidad = _clienteRepositorio.ObtenerPorId(id);
            if (entidad == null) { return NotFound(); }
            await _clienteRepositorio.EliminarAsync(entidad);
            return NoContent();
        }
    }
}
