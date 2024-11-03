﻿using BackEnd.Modules.ModuloMantenimiento;
using BackEnd.Modules.ModuloSeguridad;

namespace BackEnd.Modules
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            DependenciesModuloSeguridad.ConfigureServices(services);
            DependenciesModuloMantenimiento.ConfigureServices(services);
            return services;
        }
    }
}
