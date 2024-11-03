using BackEnd.Modules.ModuloSeguridad.Roles.Entities;
using BackEnd.Modules.ModuloSeguridad.Roles.Interfaces;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Interfaces;

namespace BackEnd.Modules.ModuloSeguridad.Roles.Service
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;
        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<List<Rol>> obtenerListaRoles()
        {
           return await _rolRepository.obtenerListaRoles();
        }
    }
}
