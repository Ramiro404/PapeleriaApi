using Microsoft.EntityFrameworkCore;
using PapeleriaApi.Migrations;
using PapeleriaApi.Modelos.Datos;
using PapeleriaApi.Modelos.Dtos;

namespace PapeleriaApi.Modelos.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
	{
        protected readonly DBContexto _dbContexto;

        public UsuarioRepositorio(DBContexto dbContexto)
        {
            _dbContexto= dbContexto;
        }
        public async Task<bool> ActualizarAsync(Usuario entidad)
        {
            _dbContexto.Entry(entidad).State = EntityState.Modified;
            await _dbContexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(Usuario entidad)
        {
            if (entidad is null)
            {
                return false;
            }
            _dbContexto.Set<Usuario>().Remove(entidad);
            await _dbContexto.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario> GuardarAsync(Usuario entidad)
        {
            await _dbContexto.Set<Usuario>().AddAsync(entidad);
            await _dbContexto.SaveChangesAsync();
            return entidad;
        }

        public IEnumerable<Usuario> Listar(int cantidad, int salto)
        {
            var usuarios = (from usuario in _dbContexto.Usuarios
                            join rol in _dbContexto.Roles on usuario.RolId equals rol.Id
                            select new Usuario
                            {
                                Id = usuario.Id,
                                ApellidoMaterno = usuario.ApellidoMaterno,
                                ApellidoPaterno = usuario.ApellidoPaterno,
                                Codigo = usuario.Codigo,
                                Email = usuario.Email,
                                Nombre = usuario.Nombre,
                                Telefono = usuario.Telefono,
                                RolId= usuario.RolId,
                                Rol = rol,
                            });

            return usuarios; 
        }

        public Usuario ObtenerPorId(int id)
        {

			return _dbContexto.Usuarios.Find(id);
			 
        }

		public UsuarioDto? ObtenerDtoPorId(int id)
		{
            var usuario =  _dbContexto.Usuarios.Find(id);
            if (usuario == null) return null;

            UsuarioDto usuarioDto = new UsuarioDto {
                Id = usuario.Id,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                Codigo = usuario.Codigo,
                Nombre = usuario.Nombre,
                Telefono = usuario.Telefono,
            };
			return usuarioDto;

		}
	}
}
