using Microsoft.EntityFrameworkCore;
using PapeleriaApi.Modelos.Datos;

namespace PapeleriaApi.Modelos.Repositorios
{
    public class CategoriaRepositorio: ICategoriaRepositorio
	{
        private readonly DBContexto _dbContexto;
        public CategoriaRepositorio(DBContexto dbContexto) {
            _dbContexto= dbContexto;
        }
        public async Task<bool> ActualizarAsync(Categoria entidad)
        {
            _dbContexto.Entry(entidad).State = EntityState.Modified;
            await _dbContexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(Categoria entidad)
        {
            if (entidad is null)
            {
                return false;
            }
            _dbContexto.Set<Categoria>().Remove(entidad);
            await _dbContexto.SaveChangesAsync();
            return true;
        }

        public async Task<Categoria> GuardarAsync(Categoria entidad)
        {
            await _dbContexto.Set<Categoria>().AddAsync(entidad);
            await _dbContexto.SaveChangesAsync();
            return entidad;
        }

        public IEnumerable<Categoria> Listar(int cantidad, int salto)
        {
			return _dbContexto.Categorias
			   .Skip(salto)
			   .Take(cantidad)
			   .ToList();
        }

        public Categoria ObtenerPorId(int id)
        {
            return _dbContexto.Categorias.Find(id);
        }
    }
}
