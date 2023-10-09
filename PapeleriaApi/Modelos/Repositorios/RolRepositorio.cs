using Microsoft.EntityFrameworkCore;
using PapeleriaApi.Modelos.Datos;

namespace PapeleriaApi.Modelos.Repositorios
{
	public class RolRepositorio : IRolRepositorio
	{
		private readonly DBContexto _dbContexto;

		public RolRepositorio(DBContexto dbContexto)
		{
			_dbContexto = dbContexto;
		}
		public async Task<bool> ActualizarAsync(Rol entidad)
		{
			_dbContexto.Entry(entidad).State = EntityState.Modified;
			await _dbContexto.SaveChangesAsync();
			return true;
		}

		public async Task<bool> EliminarAsync(Rol entidad)
		{
			if (entidad is null)
			{
				return false;
			}
			_dbContexto.Set<Rol>().Remove(entidad);
			await _dbContexto.SaveChangesAsync();
			return true;
		}

		public async Task<Rol> GuardarAsync(Rol entidad)
		{
			await _dbContexto.Set<Rol>().AddAsync(entidad);
			await _dbContexto.SaveChangesAsync();
			return entidad;
		}

		public IEnumerable<Rol> Listar(int cantidad, int salto)
		{
			return _dbContexto.Roles
				.Skip(salto)
				.Take(cantidad)
				.ToList();
		}

		public Rol ObtenerPorId(int id)
		{
			return _dbContexto.Roles.Find(id);
		}
	}
}
