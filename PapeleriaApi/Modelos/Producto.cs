using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PapeleriaApi.Modelos
{
    public class Producto
    {
        public Producto() { 
            this.OrdenesDeCompra = new HashSet<OrdenDeCompra>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Precision(18,2)]
        public decimal PrecioUnitario { get; set; }
        public string CodigoDeBarras { get; set; }
        public int ExistenciaEnVenta { get; set;}
        public int ExistenciaEnBodega { get; set; }

        public Nullable<int> CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        public Nullable<int> MarcaId { get; set; }
        public virtual Marca Marca { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrdenDeCompra> OrdenesDeCompra { get; set; }
    }
}
