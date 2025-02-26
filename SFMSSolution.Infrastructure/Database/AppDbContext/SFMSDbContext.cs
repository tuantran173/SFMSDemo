using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.Configurations;
using SFMSSolution.Infrastructure.Database.SFMSDbContext;

namespace SFMSSolution.Infrastructure.Database.AppDbContext
{
    public class SFMSDbContext : DbContext
    {
        public SFMSDbContext(DbContextOptions<SFMSDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            // Gọi phương thức Seed từ SFMSDbInitializer
            SFMSDbInitializer.Seed(modelBuilder);
        }
    }
}
