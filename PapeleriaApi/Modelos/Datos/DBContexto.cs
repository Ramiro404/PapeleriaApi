using Microsoft.EntityFrameworkCore;

namespace PapeleriaApi.Modelos.Datos
{
    public class DBContexto: DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options): base(options) { 
            
        }

        public DbSet<Producto> Productos { get; set; }
		public DbSet<Rol> Roles { get; set; }
		public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set;}
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<OrdenDeCompra> OrdenesDeCompra { get; set; }
    }
}
