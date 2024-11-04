using BackEnd.Modules.ModuloMantenimiento.Producto.Entities;

namespace BackEnd.Modules.ModuloMantenimiento.Producto.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<ProductoDto>> ListarProductos();
        Task<int> EliminarProducto(int productId);
        Task<int> ActualizarProducto(Productto productto);
        Task<int> InsertarProducto(Productto productto);
    }
}
