using PapeleriaApi.Modelos.Dtos;

namespace PapeleriaApi.Modelos.Repositorios
{
	public interface IOrdenDeCompraRepositorio: ICrudRepositorio<OrdenDeCompra>
	{
		public Task<IEnumerable<OrdenDeCompra>> ListarOrdenes();
		public OrdenDeCompraDto ListarPorFolio(string folio);
		public Task<OrdenDeCompra[]> GuardarMultiples(OrdenDeCompra[] ordenesDeCompra);
	}
}
