using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PapeleriaApi.Modelos
{
	public class Rol
	{
		public Rol() { 
			this.Usuarios = new HashSet<Usuario>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Tipo { get; set; }

		[JsonIgnore]
		public virtual ICollection<Usuario> Usuarios { get; set; }
	}
}
