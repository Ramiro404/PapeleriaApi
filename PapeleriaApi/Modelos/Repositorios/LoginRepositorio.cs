using PapeleriaApi.Modelos.Datos;
using System.Security.Cryptography;

namespace PapeleriaApi.Modelos.Repositorios
{
	public class LoginRepositorio : ILoginRepositorio
	{
		private readonly DBContexto _dbContexto;

		public LoginRepositorio(
			DBContexto dBContexto)
		{
			_dbContexto = dBContexto;
		}
		public Usuario Autenticar(LoginUsuario loginUsuario)
		{
			var usuarioActual = _dbContexto.Usuarios.Where(u => u.Email == loginUsuario.Correo).FirstOrDefault();
			usuarioActual.Rol = _dbContexto.Roles.Where( r => r.Id == usuarioActual.RolId).FirstOrDefault();
			Console.WriteLine(usuarioActual.ToString());
			if (usuarioActual is null) return null;
			
			bool verificado = BCrypt.Net.BCrypt.Verify(loginUsuario.Password, usuarioActual.PasswordHash);
			if (verificado)
			{
				return usuarioActual;	
			}
				return null;
		}
	}
}
