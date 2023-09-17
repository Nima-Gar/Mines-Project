using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
