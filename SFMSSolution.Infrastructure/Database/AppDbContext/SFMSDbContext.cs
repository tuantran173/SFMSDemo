using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.Configurations;
using Microsoft.AspNetCore.Identity;
using SFMSSolution.Infrastructure.Database.SFMSDbContext;

namespace SFMSSolution.Infrastructure.Database.AppDbContext
{
    public class SFMSDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid> // Kế thừa từ IdentityDbContext
    {
        public SFMSDbContext(DbContextOptions<SFMSDbContext> options) : base(options) { }

        // Các DbSet cho entity tùy chỉnh
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<FacilityTimeSlot> FacilityTimeSlots { get; set; }
        public DbSet<FacilityPrice> FacilityPrices { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Gọi base để cấu hình Identity
            modelBuilder.UseOpenIddict();
            // Áp dụng các configuration cho entity
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityPriceConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityTimeSlotConfiguration());

            // Gọi phương thức Seed từ SFMSDbInitializer
            SFMSDbInitializer.Seed(modelBuilder);
        }
    }
}