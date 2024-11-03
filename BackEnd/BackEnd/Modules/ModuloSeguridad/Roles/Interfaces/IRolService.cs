using BackEnd.Modules.ModuloSeguridad.Roles.Entities;

namespace BackEnd.Modules.ModuloSeguridad.Roles.Interfaces
{
    public interface IRolService
    {
        Task<List<Rol>> obtenerListaRoles();
    }
}
