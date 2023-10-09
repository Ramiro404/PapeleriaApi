using System.ComponentModel.DataAnnotations;

namespace PapeleriaApi.Modelos.Dtos
{
	public class UsuarioDto
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string ApellidoMaterno { get; set; }
		public string ApellidoPaterno { get; set; }
		public string Email { get; set; }
		public string Codigo { get; set; }
		public string Telefono { get; set; }
		public Rol Rol { get; set; }

	}
}
