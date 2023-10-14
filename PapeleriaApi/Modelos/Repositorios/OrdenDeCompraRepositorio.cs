using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PapeleriaApi.Modelos.Datos;
using PapeleriaApi.Modelos.Dtos;
using System.Collections.Generic;
using System.Text;

namespace PapeleriaApi.Modelos.Repositorios
{
	public class OrdenDeCompraRepositorio : IOrdenDeCompraRepositorio
	{
		private DBContexto _dbContexto;
		private IProductoRepositorio _productoRepositorio;
		private readonly IUsuarioRepositorio _usuarioRepositorio;
		private readonly IClienteRepositorio _clienteRepositorio;
		private readonly ICategoriaRepositorio _categoriaRepositorio;
		private readonly IMarcaRepositorio _marcaRepositorio;

		public OrdenDeCompraRepositorio(
			DBContexto dBContexto,
			IProductoRepositorio productoRepositorio,
			IUsuarioRepositorio usuarioRepositorio,
			IClienteRepositorio clienteRepositorio,
			ICategoriaRepositorio categoriaRepositorio,
			IMarcaRepositorio marcaRepositorio
			)
		{
			_dbContexto = dBContexto;
			_productoRepositorio = productoRepositorio;
			_usuarioRepositorio = usuarioRepositorio;
			_clienteRepositorio = clienteRepositorio;
			_categoriaRepositorio = categoriaRepositorio;
			_marcaRepositorio = marcaRepositorio;
		}

		public async Task<bool> ActualizarAsync(OrdenDeCompra entidad)
		{
			_dbContexto.Entry(entidad).State = EntityState.Modified;
			await _dbContexto.SaveChangesAsync();
			return true;
		}

		public async Task<bool> EliminarAsync(OrdenDeCompra entidad)
		{
			if (entidad is null)
			{
				return false;
			}
			_dbContexto.Set<OrdenDeCompra>().Remove(entidad);
			await _dbContexto.SaveChangesAsync();
			return true;
		}

		public IEnumerable<OrdenDeCompra> Listar(int cantidad, int salto)
		{
			return _dbContexto.OrdenesDeCompra
				.Skip(salto)
				.Take(cantidad)
				.ToList();
		}

		public async Task<OrdenDeCompra> GuardarAsync(OrdenDeCompra entidad)
		{

			await _dbContexto.Set<OrdenDeCompra>().AddAsync(entidad);
			await _dbContexto.SaveChangesAsync();
			return entidad;
		}

		public OrdenDeCompraDto ListarPorFolio(string folio)
		{
			var ordenes = _dbContexto.OrdenesDeCompra.Where(o => o.Folio == folio).ToList();
			List<Producto> productos = new List<Producto> { };
			Decimal subTotalAPagar = 0;
			foreach (var orden in ordenes)
			{
				var producto = _productoRepositorio.ObtenerProductoPorId((int)orden.ProductoId);
				int categoriaId = producto.CategoriaId.GetValueOrDefault();
				producto.Categoria = _categoriaRepositorio.ObtenerPorId(categoriaId);
				int marcaId = producto.MarcaId.GetValueOrDefault();
				producto.Marca = _marcaRepositorio.ObtenerPorId(marcaId);
				productos.Add(producto);
				subTotalAPagar += producto.PrecioUnitario * orden.Cantidad;
			}
			int vendedorId = ordenes[0].UsuarioId.GetValueOrDefault();
			var vendedor = _usuarioRepositorio.ObtenerPorId(vendedorId);
			int clienteId = ordenes[0].ClienteId.GetValueOrDefault();
			var cliente = _clienteRepositorio.ObtenerPorId(clienteId);

			OrdenDeCompraDto ordenDeCompraDto = new OrdenDeCompraDto
			{
				FechaYHora = ordenes[0].FechaYHora,
				Cliente = cliente,
				Folio = ordenes[0].Folio,
				Productos = productos,
				SubTotalAPagar = subTotalAPagar,
				TotalAPagar = subTotalAPagar,
				TotalProductos = productos.Count,
				Vendedor = vendedor
			};
			return ordenDeCompraDto;
		}

		public async Task<IEnumerable<OrdenDeCompra>> ListarOrdenes()
		{

			return _dbContexto.OrdenesDeCompra.ToList();
		}

		public OrdenDeCompra ObtenerPorId(int id)
		{
			return _dbContexto.OrdenesDeCompra.Find();
		}

		public async Task<OrdenDeCompra[]> GuardarMultiples(OrdenDeCompra[] ordenesDeCompra)
		{
			var folioMaximo = _dbContexto.OrdenesDeCompra.Select(orden => orden.Folio).Max();
			int folioMaximoDecimal = Convert.ToInt32(folioMaximo, 16) + 1;
			string folioMaximoHex = folioMaximoDecimal.ToString("X");
			foreach (OrdenDeCompra orden in ordenesDeCompra)
			{
				orden.Folio = folioMaximoHex;
			}
			await _dbContexto.Set<OrdenDeCompra>().AddRangeAsync(ordenesDeCompra);
			await _dbContexto.SaveChangesAsync();
			return ordenesDeCompra;
		}


	}
}
