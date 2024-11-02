using BackEnd.Modules.ModuloSeguridad.Usuarios.Interfaces;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Repository;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Service;

namespace BackEnd.Modules.ModuloSeguridad.Usuarios
{
    public class DependenciesUsuarios
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
