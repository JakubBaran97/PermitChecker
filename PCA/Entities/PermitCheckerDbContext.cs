using Microsoft.EntityFrameworkCore;
using PermitChecker.Models;

namespace PCA.Entities
{
    public class PermitCheckerDbContext : DbContext
    {
        private string _connectionString = "server=(localdb)\\local;database=PermitCheckerDbContext;Trusted_connection=true";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<Building> Building { get; set; }

        public DbSet<Permission> Permission { get; set; }
    }
}
