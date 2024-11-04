using BackEnd.Modules.ModuloMantenimiento.Producto.Entities;
using BackEnd.Modules.ModuloMantenimiento.Producto.Interfaces;

namespace BackEnd.Modules.ModuloMantenimiento.Producto.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<ProductoDto>> ListarProductos()
        {
           return await _productoRepository.ListarProductos();
        }

        public async Task<int> ActualizarProducto(Productto productto)
        {
            return await _productoRepository.ActualizarProducto(productto);
        }

        public async Task<int> EliminarProducto(int productId)
        {
            return await _productoRepository.EliminarProducto(productId);
        }

        public async Task<int> InsertarProducto(Productto productto)
        {
            return await _productoRepository.InsertarProducto(productto);
        }
    }
}
