namespace PapeleriaApi.Modelos.Repositorios
{
    public interface IProductoRepositorio
    {
        IEnumerable<Producto> ListarProductos(int cantidad, int salto);
        Producto ObtenerProductoPorId(int id);
        Task<Producto> GuardarProductoAsync(Producto producto);
        Task<bool> ActualizarProductoAsync(Producto producto);
        Task<bool> EliminarProductoAsync(Producto producto);

    }
}
