using BackEnd.Modules.ModuloSeguridad.Usuarios.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Context
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Usuario> Users { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}
