using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;
using System;

namespace SFMSSolution.Infrastructure.Database.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.BookingDate)
                   .IsRequired();

            builder.Property(b => b.StartTime)
                   .HasColumnType("time")
                   .IsRequired(false);

            builder.Property(b => b.EndTime)
                   .HasColumnType("time")
                   .IsRequired(false);
            builder.Property(b => b.CustomerEmail)
                    .HasMaxLength(255)
                    .IsRequired();
            builder.Property(b => b.Note)
                   .HasMaxLength(1000);
            builder.Property(b => b.ImageUrl)
                   .HasMaxLength(500);

            builder.Property(b => b.Status)
                   .IsRequired()
                   .HasConversion(
                        v => v.ToString(),
                        v => (BookingStatus)Enum.Parse(typeof(BookingStatus), v))
                   .HasMaxLength(50);

            builder.Property(b => b.PaymentMethod)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(b => b.FinalPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(b => b.CustomerName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(b => b.CustomerPhone)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasOne(b => b.Facility)
                   .WithMany()
                   .HasForeignKey(b => b.FacilityId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.User)
                   .WithMany()
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.FacilityTimeSlot)
                   .WithMany()
                   .HasForeignKey(b => b.FacilityTimeSlotId)
                   .OnDelete(DeleteBehavior.Restrict);

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
