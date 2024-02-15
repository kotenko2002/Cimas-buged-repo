using Cimas.Domain.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cimas.Infrastructure.Common
{
    public class CimasDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public CimasDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(builder =>
            {
                builder.Property(c => c.Name).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
