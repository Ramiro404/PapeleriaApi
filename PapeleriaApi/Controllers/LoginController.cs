using Microsoft.AspNetCore.Mvc;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Dtos;
using PapeleriaApi.Modelos.Repositorios;
using PapeleriaApi.Servicios;
using PapeleriaApi.Utilidades;

namespace PapeleriaApi.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly ILoginRepositorio loginRepositorio;
		private readonly IServicioJwt servicioJwt;

		public LoginController(
			ILoginRepositorio loginRepositorio,
			IServicioJwt servicioJwt)
		{
			this.loginRepositorio = loginRepositorio;
			this.servicioJwt = servicioJwt;
		}
		[HttpPost]
		public IActionResult Login(LoginUsuario loginUsuario)
		{
			try
			{
				
				var usuario = loginRepositorio.Autenticar(loginUsuario);
				if (usuario == null)
				{
					return NotFound();
				}
				var mapper = MapperConfig.InicializarAutoMapper();
				UsuarioDto usuarioDto = mapper.Map<UsuarioDto>(usuario);
				var token = servicioJwt.Generar(usuarioDto);
				return Ok(new { Token= token});
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return StatusCode(500, ex.Message);
			}
			
		}
	}
}
