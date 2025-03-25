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
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<FacilityTimeSlot> FacilityTimeSlots { get; set; }
        public DbSet<FacilityPrice> FacilityPrices { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<EmailTemplate> Emails {  get; set; }  
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityPriceConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityTimeSlotConfiguration());
            modelBuilder.ApplyConfiguration(new PriceConfiguration());

            // Gọi phương thức Seed từ SFMSDbInitializer
            SFMSDbInitializer.Seed(modelBuilder);
        }
    }
}
