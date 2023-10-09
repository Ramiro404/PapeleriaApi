using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PapeleriaApi.Modelos
{
    public class OrdenDeCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime FechaYHora { get; set; }
        public int Cantidad { get; set; }
        public string Folio { get; set; }

        public Nullable<int> ProductoId { get;set; }
        public virtual Producto Producto { get; set; }

        public Nullable<int> ClienteId { get; set; }
        public virtual Cliente Cliente { get;set; }

        public Nullable<int> UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
