namespace PapeleriaApi.Modelos.Repositorios
{
    public interface ICrudRepositorio<E>
    {
        IEnumerable<E> Listar(int cantidad, int salto);
        E ObtenerPorId(int id);
        Task<E> GuardarAsync(E entidad);
        Task<bool> ActualizarAsync(E entidad);
        Task<bool> EliminarAsync(E entidad);
    }
}
