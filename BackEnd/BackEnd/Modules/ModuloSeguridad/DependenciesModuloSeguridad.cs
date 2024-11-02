using BackEnd.Modules.ModuloSeguridad.Usuarios;

namespace BackEnd.Modules.ModuloSeguridad
{
    public class DependenciesModuloSeguridad
    {
        public static void ConfigureServices(IServiceCollection service)
        {

            DependenciesUsuarios.ConfigureServices(service);
        }
    }
}
