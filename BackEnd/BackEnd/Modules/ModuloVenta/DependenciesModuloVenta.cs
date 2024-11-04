using BackEnd.Modules.ModuloVenta.Ventas;

namespace BackEnd.Modules.ModuloVenta
{
    public class DependenciesModuloVenta
    {
        public static void ConfigureServices(IServiceCollection service)
        {
            DependenciesVenta.ConfigureServices(service);
        }
        
    }
}
