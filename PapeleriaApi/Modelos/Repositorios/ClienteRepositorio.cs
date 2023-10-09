using Microsoft.EntityFrameworkCore;
using PapeleriaApi.Modelos.Datos;

namespace PapeleriaApi.Modelos.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
	{
        private readonly DBContexto _dbContexto;
        public ClienteRepositorio(DBContexto dbContexto) { 
            _dbContexto= dbContexto;
        }
        public async Task<bool> ActualizarAsync(Cliente entidad)
        {
            _dbContexto.Entry(entidad).State = EntityState.Modified;
            await _dbContexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(Cliente entidad)
        {
            if (entidad is null)
            {
                return false;
            }
            _dbContexto.Set<Cliente>().Remove(entidad);
            await _dbContexto.SaveChangesAsync();
            return true;
        }

        public async Task<Cliente> GuardarAsync(Cliente entidad)
        {
            await _dbContexto.Set<Cliente>().AddAsync(entidad);
            await _dbContexto.SaveChangesAsync();
            return entidad;
        }

        public IEnumerable<Cliente> Listar(int cantidad, int salto)
        {
            return _dbContexto.Clientes
                .Skip(salto)
                .Take(cantidad)
                .ToList();
        }

        public Cliente ObtenerPorId(int id)
        {
            return _dbContexto.Clientes.Find(id);
        }
    }
}
