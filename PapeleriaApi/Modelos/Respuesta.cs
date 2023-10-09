namespace PapeleriaApi.Modelos
{
	public class RespuestaApiMultiple<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Result { get; set; }

	}
}
