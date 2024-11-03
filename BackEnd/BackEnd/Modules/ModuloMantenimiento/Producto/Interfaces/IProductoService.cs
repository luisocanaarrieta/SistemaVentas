using BackEnd.Modules.ModuloMantenimiento.Producto.Entities;

namespace BackEnd.Modules.ModuloMantenimiento.Producto.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> ListarProductos();
    }
}
