using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;

namespace BackEnd.Modules.ModuloSeguridad.Login.Interfaces
{
    public interface ILoginService
    {
        Task<Usuario> ValidateUser(Usuario usuario);
    }
}
