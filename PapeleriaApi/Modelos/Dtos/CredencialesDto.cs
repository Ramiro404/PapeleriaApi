using System.ComponentModel.DataAnnotations;

namespace PapeleriaApi.Modelos.Dtos
{
	public class CredencialesDto
	{
		[Required(ErrorMessage = "Correo requerido")]
		[EmailAddress(ErrorMessage = "Correo no valido")]
		public string Correo { get; set; }
		[Required(ErrorMessage = "Password requerido")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
