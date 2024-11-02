using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Models;
using BackEnd.Repositories;

namespace BackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usuarioRepository;
        public UserService(IUserRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task SaveUser(Users usuario)
        {
            await _usuarioRepository.SaveUser(usuario);
        }

        public async Task UpdatePassword(Users usuario)
        {
            await _usuarioRepository.UpdatePassword(usuario);
        }

        public async Task<bool> ValidateExistence(Users usuario)
        {
            return await _usuarioRepository.ValidateExistence(usuario);
        }

        public async Task<Users> ValidatePassword(int idUsuario, string passwordAnterior)
        {
            return await _usuarioRepository.ValidatePassword(idUsuario, passwordAnterior);
        }

        public async Task<Users> ValidateUser(Users usuario)
        {
            return await _usuarioRepository.ValidateUser(usuario);
        }
    }
}
