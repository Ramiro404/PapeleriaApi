namespace PapeleriaApi.Modelos.Dtos
{
	public class RespuestaApiPlural<ENTIDAD>
	{
		public int Salto { get; set; }
		public int Cantidad { get; set; }
		public IEnumerable<ENTIDAD>? Datos { get; set; }
		public string? Error { get; set; }
	}
}
