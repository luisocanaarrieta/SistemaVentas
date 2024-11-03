
using BackEnd.Modules.ModuloSeguridad.Roles.Interfaces;
using BackEnd.Modules.ModuloSeguridad.Roles.Repository;
using BackEnd.Modules.ModuloSeguridad.Roles.Service;

namespace BackEnd.Modules.ModuloSeguridad.Roles
{
    public class DependenciesRoles
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IRolRepository, RolRepository>();
        }
    }
}
