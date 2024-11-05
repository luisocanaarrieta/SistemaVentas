using BackEnd.Modules.ModuloSeguridad.Roles.Entities;

namespace BackEnd.Modules.ModuloSeguridad.Roles.Interfaces
{
    public interface IRolRepository
    {
        Task<List<Rol>> obtenerListaRoles();
        Task<List<ModuloXRol>> ModuloXRol(int rolId);

    }
}
