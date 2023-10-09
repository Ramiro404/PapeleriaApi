using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapeleriaApi.Modelos.Dtos;
using PapeleriaApi.Modelos.Repositorios;

namespace PapeleriaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VendedorUsuarioController : ControllerBase
	{
		private readonly IInicioSesionRepositorio vendedorUsuarioRepositorio;

		public VendedorUsuarioController(
			IInicioSesionRepositorio vendedorUsuarioRepositorio)
		{
			this.vendedorUsuarioRepositorio = vendedorUsuarioRepositorio;
		}

		[HttpPost("login")]
		public async Task<dynamic> VerificarCredenciales(CredencialesDto credenciales)
		{
			var resultado = await this.vendedorUsuarioRepositorio.VerificarCredenciales(credenciales);
			return resultado;
		}
	}
}
