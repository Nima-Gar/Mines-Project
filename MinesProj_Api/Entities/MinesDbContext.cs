using Entities.Models;
using Microsoft.EntityFrameworkCore;
using MinesApi.Models;

namespace Entities
{
    public class MinesDbContext : DbContext
    {
        public MinesDbContext(DbContextOptions<MinesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mine> Mines { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<MineType> MineTypes { get; set; }
        public DbSet<OwnershipType> OwnershipTypes { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PhoneNumType> PhoneNumTypes { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
