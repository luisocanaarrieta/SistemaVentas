using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;

namespace BackEnd.Modules.ModuloSeguridad.Login.Interfaces
{
    public interface ILoginRepository
    {
        Task<Usuario> ValidateUser(Usuario usuario);
       
    }
}
