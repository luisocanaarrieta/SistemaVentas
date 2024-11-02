using BackEnd.Context;
using BackEnd.Domain.IRepositories;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AplicationDbContext _context;
        public UserRepository(AplicationDbContext context) 
        {
            _context = context;
            
        }
        public async Task SaveUser(Users usuario)
        {
            _context.Users.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePassword(Users usuario)
        {
            _context.Users.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateExistence(Users usuario)
        {
            var validateExistence = await _context.Users.AnyAsync(x => x.USER_NAME == usuario.USER_NAME);
            return validateExistence;
        }

        public async Task<Users> ValidatePassword(int idUsuario, string passwordAnterior)
        {
            var usuario = await _context.Users.Where(x => x.ID == idUsuario && x.USER_PASSWORD == passwordAnterior).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<Users> ValidateUser(Users usuario)
        {
            var user = await _context.Users.Where(x =>
            x.USER_NAME == usuario.USER_NAME &&
            x.USER_PASSWORD == usuario.USER_PASSWORD)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
