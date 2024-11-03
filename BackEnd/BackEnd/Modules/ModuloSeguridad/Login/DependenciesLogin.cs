using BackEnd.Modules.ModuloSeguridad.Login.Interfaces;
using BackEnd.Modules.ModuloSeguridad.Login.Repository;
using BackEnd.Modules.ModuloSeguridad.Login.Service;

namespace BackEnd.Modules.ModuloSeguridad.Login
{
    public class DependenciesLogin
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginRepository, LoginRepository>();
        }
    }
}
