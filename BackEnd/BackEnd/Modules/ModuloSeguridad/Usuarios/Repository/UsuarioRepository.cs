using BackEnd.Context;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;
using BackEnd.Modules.ModuloSeguridad.Usuarios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Modules.ModuloSeguridad.Usuarios.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AplicationDbContext _context;
        public UsuarioRepository(AplicationDbContext context)
        {
            _context = context;

        }
        public async Task SaveUser(Usuario usuario)
        {
            _context.Users.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePassword(Usuario usuario)
        {
            _context.Users.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateExistence(Usuario usuario)
        {
            var validateExistence = await _context.Users.AnyAsync(x => x.USER_NAME == usuario.USER_NAME);
            return validateExistence;
        }

        public async Task<Usuario> ValidatePassword(int idUsuario, string passwordAnterior)
        {
            var usuario = await _context.Users.Where(x => x.ID == idUsuario && x.USER_PASSWORD == passwordAnterior).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            var user = await _context.Users.Where(x =>
            x.USER_NAME == usuario.USER_NAME &&
            x.USER_PASSWORD == usuario.USER_PASSWORD)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
