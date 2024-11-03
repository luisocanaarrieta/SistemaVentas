using BackEnd.Modules.ModuloMantenimiento.Categoria;
using BackEnd.Modules.ModuloMantenimiento.Producto;

namespace BackEnd.Modules.ModuloMantenimiento
{
    public class DependenciesModuloMantenimiento
    {
        public static void ConfigureServices(IServiceCollection service)
        {

            DependenciesCategoria.ConfigureServices(service);
            DependenciesProducto.ConfigureServices(service);

        }
        
    }
}
