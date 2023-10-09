using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PapeleriaApi.Modelos.Datos;
using PapeleriaApi.Modelos.Dtos;
using PapeleriaApi.Servicios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PapeleriaApi.Modelos.Repositorios
{
    public interface IInicioSesionRepositorio
	{
		public Task<dynamic> VerificarCredenciales(CredencialesDto credenciales);
	}
	public class InicioSesionRepositorio : IInicioSesionRepositorio
	{
		private readonly DBContexto dbContexto;
		private readonly UserManager<Usuario> userManager;
		private readonly IConfiguration configuration;

		public InicioSesionRepositorio(
			DBContexto _dbContexto, 
			UserManager<Usuario> userManager,
			IConfiguration configuration)
		{
			dbContexto = _dbContexto;
			this.userManager = userManager;
			this.configuration = configuration;
		}
		public async Task<Object> VerificarCredenciales(CredencialesDto credenciales)
		{
			var usuario = await dbContexto.Usuarios
				.SingleOrDefaultAsync(vendedor => vendedor.Email == credenciales.Correo);
			//var esPasswordCorrecto = await userManager.CheckPasswordAsync(usuario, credenciales.Password);
			bool esPasswordCorrecto = (usuario.PasswordHash == credenciales.Password);
			if (usuario == null)
			{
				return "Correo o password incorrecto";
			}
			if(usuario.PasswordHash != credenciales.Password)
			{
				return "Correo o password incorrecto";
			}
			var jwt = configuration.GetSection("Jwt").Get<Jwt>();
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
				new Claim("id", usuario.Id.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
			var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				jwt.Issuer,
				jwt.Audience,
				claims,
				expires: DateTime.Now.AddMinutes(5),
				signingCredentials: signIn);

			var jwtResult = new JwtSecurityTokenHandler().WriteToken(token);
			return new RespuestaApiMultiple<string>{ 
			Success = true,
			Message = "Exito",
			Result = jwtResult};
		}
	}
}
