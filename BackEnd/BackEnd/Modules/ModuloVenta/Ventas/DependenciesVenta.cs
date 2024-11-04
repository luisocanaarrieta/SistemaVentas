using BackEnd.Modules.ModuloVenta.Ventas.Interfaces;
using BackEnd.Modules.ModuloVenta.Ventas.Repository;
using BackEnd.Modules.ModuloVenta.Ventas.Service;

namespace BackEnd.Modules.ModuloVenta.Ventas
{
    public class DependenciesVenta
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IVentaRepository, VentaRepository>();
        }
    }
}
