using PapeleriaApi.Modelos.Dtos;

namespace PapeleriaApi.Modelos.Repositorios
{
	public interface IUsuarioRepositorio: ICrudRepositorio<Usuario>
	{
		public UsuarioDto ObtenerDtoPorId(int id);
	}
}
