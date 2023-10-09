using AutoMapper;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Dtos;

namespace PapeleriaApi.Utilidades
{
	public class MapperConfig
	{
		public static Mapper InicializarAutoMapper()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Usuario, UsuarioDto>();
			});
		
			var mapper = new Mapper(config);
			return mapper;
		}
	}
}
