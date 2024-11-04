using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Interfaces;

namespace BackEnd.Modules.ModuloSeguridad.Usuarios.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<int> ActualizarUsuario(Usuario usuario)
        {
           return await _usuarioRepository.ActualizarUsuario(usuario);
        }

        public async Task<int> EliminarUsuario(int userId)
        {
           return await _usuarioRepository.EliminarUsuario(userId);
        }

        public async Task<List<UsuarioDto>> ListarUsuarios()
        {
            return await _usuarioRepository.ListarUsuarios();
        }

        public async Task<int> SaveUser(Usuario usuario)
        {
           return await _usuarioRepository.SaveUser(usuario);
        }

        public async Task<int> ValidateExistence(Usuario usuario)
        {
            return await _usuarioRepository.ValidateExistence(usuario);
        }    
    }
}
