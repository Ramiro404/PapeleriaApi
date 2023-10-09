using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PapeleriaApi.Modelos
{
    public class Categoria
    {
        public Categoria() { 
            this.Productos = new HashSet<Producto>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; }
        [JsonIgnore]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
