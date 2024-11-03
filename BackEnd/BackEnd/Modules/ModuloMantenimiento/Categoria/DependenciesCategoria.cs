
using BackEnd.Modules.ModuloMantenimiento.Categoria.Interfaces;
using BackEnd.Modules.ModuloMantenimiento.Categoria.Repository;
using BackEnd.Modules.ModuloMantenimiento.Categoria.Service;

namespace BackEnd.Modules.ModuloMantenimiento.Categoria
{
    public class DependenciesCategoria
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        }
    }
}
