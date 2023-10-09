namespace PapeleriaApi.Modelos.Repositorios
{
	public interface ILoginRepositorio
	{
		public Usuario Autenticar(LoginUsuario loginUsuario);
	}
}
