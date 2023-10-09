using Microsoft.IdentityModel.Tokens;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Dtos;
using PapeleriaApi.Modelos.Repositorios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PapeleriaApi.Servicios
{
	public interface IServicioJwt
	{
		public string Generar(UsuarioDto usuario);
	}
	public class ServicioJwt: IServicioJwt
	{
		private readonly IConfiguration configuration;
		private readonly IUsuarioRepositorio _usuarioRepositorio;

		public ServicioJwt(
			IConfiguration configuration,
			IUsuarioRepositorio usuarioRepositorio)
		{
			this.configuration = configuration;
			this._usuarioRepositorio = usuarioRepositorio;
		}

		public string Generar(UsuarioDto usuario)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			// Crear los claims
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
				new Claim(ClaimTypes.GivenName, usuario.Nombre),
				new Claim(ClaimTypes.Surname, usuario.ApellidoPaterno),
				new Claim(ClaimTypes.Email, usuario.Email),
				new Claim(ClaimTypes.Role, usuario.Rol.Tipo)
			};


			// Crear el token

			var token = new JwtSecurityToken(
				configuration["Jwt:Issuer"],
				configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(60),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		
	}
}
