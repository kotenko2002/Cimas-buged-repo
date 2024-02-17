using Cimas.Domain.Companies;
using Cimas.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Common
{
    public class CimasDbContext : IdentityDbContext<User>
    {
        public DbSet<Company> Companies { get; set; }

        public CimasDbContext(DbContextOptions<CimasDbContext> options) : base(options)
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
