using PapeleriaApi.Modelos.Datos;
using Microsoft.EntityFrameworkCore;

namespace PapeleriaApi.Modelos.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        protected readonly DBContexto dbContexto;
        public ProductoRepositorio(DBContexto dbContexto) => this.dbContexto = dbContexto;

        public async Task<bool> ActualizarProductoAsync(Producto producto)
        {
            this.dbContexto.Entry(producto).State = EntityState.Modified;
            await this.dbContexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarProductoAsync(Producto producto)
        {
            if(producto is null)
            {
                return false;
            }
            this.dbContexto.Set<Producto>().Remove(producto);
            await this.dbContexto.SaveChangesAsync();
            return true;
        }

        public async Task<Producto> GuardarProductoAsync(Producto producto)
        {
            await this.dbContexto.Set<Producto>().AddAsync(producto);
            await this.dbContexto.SaveChangesAsync();
            return producto;
        }

        public IEnumerable<Producto> ListarProductos(int cantidad, int salto)
        {
            return this.dbContexto.Productos
                .Skip(salto)
                .Take(cantidad)
                .ToList();
        }

        public Producto ObtenerProductoPorId(int id)
        {
            return this.dbContexto.Productos.Find(id);
        }
    }
}
