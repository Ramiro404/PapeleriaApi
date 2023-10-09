using System.ComponentModel.DataAnnotations;

namespace PapeleriaApi.Modelos.Dtos
{
	public class OrdenDeCompraDto
	{
		public int Id { get; set; }
		public DateTime FechaYHora { get; set; }
		public string Folio { get; set; }
		public Cliente Cliente { get; set; }
		public Usuario Vendedor { get; set; }
		public IQueryable<Producto> ProductoLista { get; set; }
		public int TotalProductos { get; set; }
		public Decimal SubTotalAPagar { get; set; }
		public Decimal TotalAPagar { get; set; }

	}
}
