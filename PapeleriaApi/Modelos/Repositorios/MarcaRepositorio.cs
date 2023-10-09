using Microsoft.EntityFrameworkCore;
using PapeleriaApi.Modelos.Datos;

namespace PapeleriaApi.Modelos.Repositorios
{
    public class MarcaRepositorio : IMarcaRepositorio
	{
        private readonly DBContexto _dbContexto;
        public MarcaRepositorio(DBContexto dbContexto) { _dbContexto= dbContexto; }

        public async Task<bool> ActualizarAsync(Marca entidad)
        {
            _dbContexto.Entry(entidad).State = EntityState.Modified;
            await _dbContexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(Marca entidad)
        {
            if (entidad is null)
            {
                return false;
            }
            _dbContexto.Set<Marca>().Remove(entidad);
            await _dbContexto.SaveChangesAsync();
            return true;
        }

        public async Task<Marca> GuardarAsync(Marca entidad)
        {
            await _dbContexto.Set<Marca>().AddAsync(entidad);
            await _dbContexto.SaveChangesAsync();
            return entidad;
        }

        public IEnumerable<Marca> Listar(int cantidad,  int salto)
        {
            return _dbContexto.Marcas
                .Skip(salto)
                .Take(cantidad)
                .ToList();
        }

        public Marca ObtenerPorId(int id)
        {
            return _dbContexto.Marcas.Find(id);
        }
    }
}
