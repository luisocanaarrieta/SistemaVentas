using BackEnd.Models;

namespace BackEnd.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task SaveUser(Users usuario);
        Task<bool> ValidateExistence(Users usuario);
        Task<Users> ValidatePassword(int idUsuario, string passwordAnterior);
        Task UpdatePassword(Users usuario);
        Task<Users> ValidateUser(Users usuario);
    }
}
