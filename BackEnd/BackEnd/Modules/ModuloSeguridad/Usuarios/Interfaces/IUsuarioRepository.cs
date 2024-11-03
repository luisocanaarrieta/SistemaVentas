using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;

namespace BackEnd.Modules.ModuloSeguridad.Usuarios.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> SaveUser(Usuario usuario);
        Task<int> ValidateExistence(Usuario usuario);
        Task<List<UsuarioDto>> ListarUsuarios();
    }
}
