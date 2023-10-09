namespace PapeleriaApi.Modelos.Dtos
{
	public class RespuestaApiSingular<ENTIDAD>
	{
		public ENTIDAD Datos { get; set; }
		public string? Error { get; set; }
	}
}
