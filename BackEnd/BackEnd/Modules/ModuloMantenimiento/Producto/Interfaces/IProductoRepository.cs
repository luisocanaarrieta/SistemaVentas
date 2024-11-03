using BackEnd.Modules.ModuloMantenimiento.Producto.Entities;

namespace BackEnd.Modules.ModuloMantenimiento.Producto.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<ProductoDto>> ListarProductos();
    }
}
