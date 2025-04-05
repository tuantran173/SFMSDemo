using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMSSolution.Domain.Enums;

namespace SFMSSolution.Infrastructure.Database.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            // Primary key
            builder.HasKey(b => b.Id);

            // Required fields
            builder.Property(b => b.BookingDate)
                   .IsRequired();

            builder.Property(b => b.StartTime)
       .HasColumnType("time")
       .IsRequired(false); // Cho phép null

            builder.Property(b => b.EndTime)
                   .HasColumnType("time")
                   .IsRequired(false); // Cho phép null

            builder.Property(b => b.Note)
                   .HasMaxLength(1000);

            // Booking status enum → string
            builder.Property(b => b.Status)
                   .IsRequired()
                   .HasConversion(
                        v => v.ToString(),
                        v => (BookingStatus)Enum.Parse(typeof(BookingStatus), v))
                   .HasMaxLength(50);

            // Foreign key: Facility
            builder.HasOne(b => b.Facility)
                   .WithMany()
                   .HasForeignKey(b => b.FacilityId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Foreign key: User
            builder.HasOne(b => b.User)
                   .WithMany()
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Foreign key: FacilityTimeSlot
            builder.HasOne(b => b.FacilityTimeSlot)
                   .WithMany()
                   .HasForeignKey(b => b.FacilityTimeSlotId)
                   .OnDelete(DeleteBehavior.Restrict);

            // === BaseEntity fields ===
            builder.Property(b => b.CreatedDate)
                   .HasColumnType("datetime(6)")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(b => b.CreatedBy)
                   .IsRequired(false);

            builder.Property(b => b.UpdatedDate)
                   .HasColumnType("datetime(6)")
                   .IsRequired(false);

            builder.Property(b => b.UpdatedBy)
                   .IsRequired(false);
        }
    }
}
