using BackEnd.Modules.ModuloMantenimiento.Producto.Interfaces;
using BackEnd.Modules.ModuloMantenimiento.Producto.Repository;
using BackEnd.Modules.ModuloMantenimiento.Producto.Service;

namespace BackEnd.Modules.ModuloMantenimiento.Producto
{
    public class DependenciesProducto
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
        }
    }
}
