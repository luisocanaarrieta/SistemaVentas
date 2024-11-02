using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BackEnd.Context
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}
