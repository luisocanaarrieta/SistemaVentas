using BackEnd.Modules.ModuloSeguridad.Login.Interfaces;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;

namespace BackEnd.Modules.ModuloSeguridad.Login.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _usuarioRepository;
        public LoginService(ILoginRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            return await _usuarioRepository.ValidateUser(usuario);
        }
    }
}
