namespace PapeleriaApi.Modelos
{
	public class RespuestaApiList<T>
	{
		public string? Error { get; set; }
		public List<T> Resultado { get; set; }
		public string Mensaje { get; set; }
	}
}
