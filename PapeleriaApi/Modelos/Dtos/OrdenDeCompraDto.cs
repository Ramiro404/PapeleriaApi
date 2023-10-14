using System.ComponentModel.DataAnnotations;

namespace PapeleriaApi.Modelos.Dtos
{
	public class OrdenDeCompraDto
	{
		public DateTime FechaYHora { get; set; }
		public string Folio { get; set; }
		public Cliente Cliente { get; set; }
		public Usuario Vendedor { get; set; }
		public List<Producto> Productos { get; set; }
		public int TotalProductos { get; set; }
		public Decimal SubTotalAPagar { get; set; }
		public Decimal TotalAPagar { get; set; }

	}
}
