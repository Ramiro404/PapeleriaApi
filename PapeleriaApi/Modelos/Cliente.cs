using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PapeleriaApi.Modelos
{
    public class Cliente
    {
        public Cliente() { 
            this.OrdenesDeCompra = new HashSet<OrdenDeCompra>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrdenDeCompra> OrdenesDeCompra { get; set; }
    }
}
