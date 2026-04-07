using Microsoft.EntityFrameworkCore;
using DriveNowApi.Models;

namespace DriveNowApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agencia> Agencias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Cliente>().HasIndex(p => p.Cpf).IsUnique();
        } 
    }
}
