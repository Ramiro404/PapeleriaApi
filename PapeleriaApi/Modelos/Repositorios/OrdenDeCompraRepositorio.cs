using Microsoft.EntityFrameworkCore;
using PapeleriaApi.Modelos.Datos;
using PapeleriaApi.Modelos.Dtos;
using System.Collections.Generic;

namespace PapeleriaApi.Modelos.Repositorios
{
	public interface IOrdenDeCompraRepositorio : ICrudRepositorio<OrdenDeCompra>
	{
		public Task<IEnumerable<OrdenDeCompra>> ListarOrdenes();

		public IEnumerable<OrdenDeCompra> ListarPorFolio(string folio);
		public Task<bool> GuaradarMultiples(OrdenDeCompra[] ordenesDeCompra);


	}
	public class OrdenDeCompraRepositorio : IOrdenDeCompraRepositorio
	{
		private DBContexto _dbContexto;
		public OrdenDeCompraRepositorio(DBContexto dBContexto) { _dbContexto = dBContexto; }

		public async Task<bool> ActualizarAsync(OrdenDeCompra entidad)
		{
			_dbContexto.Entry(entidad).State = EntityState.Modified;
			await _dbContexto.SaveChangesAsync();
			return true;
		}

		public async Task<bool> EliminarAsync(OrdenDeCompra entidad)
		{
			if (entidad is null)
			{
				return false;
			}
			_dbContexto.Set<OrdenDeCompra>().Remove(entidad);
			await _dbContexto.SaveChangesAsync();
			return true;
		}

		public IEnumerable<OrdenDeCompra> Listar(int cantidad, int salto)
		{
			IEnumerable<OrdenDeCompra> orden = null;
			return orden;
		}

		public async Task<OrdenDeCompra> GuardarAsync(OrdenDeCompra entidad)
		{

			await _dbContexto.Set<OrdenDeCompra>().AddAsync(entidad);
			await _dbContexto.SaveChangesAsync();
			return entidad;
		}

		public IEnumerable<OrdenDeCompra> ListarPorFolio(string folio)
		{
			return _dbContexto.OrdenesDeCompra.Where(o => o.Folio == folio).ToList();
		}

		public async Task<IEnumerable<OrdenDeCompra>> ListarOrdenes()
		{
			return _dbContexto.OrdenesDeCompra.ToList();
		}

		public OrdenDeCompra ObtenerPorId(int id)
		{
			return _dbContexto.OrdenesDeCompra.Find();
		}

		public async Task<bool> GuaradarMultiples(OrdenDeCompra[] ordenesDeCompra)
		{
			await _dbContexto.Set<OrdenDeCompra>().AddRangeAsync(ordenesDeCompra);
			await _dbContexto.SaveChangesAsync();
			return true;
		}

		IEnumerable<OrdenDeCompra> ICrudRepositorio<OrdenDeCompra>.Listar(int cantidad, int posicion)
		{
			throw new NotImplementedException();
		}
	}
}
