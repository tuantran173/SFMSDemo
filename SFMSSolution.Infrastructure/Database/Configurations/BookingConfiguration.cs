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
            // Khoá chính (đã có trong BaseEntity, nhưng xác nhận lại)
            builder.HasKey(b => b.Id);

            // BookingDate, StartTime, EndTime là bắt buộc
            builder.Property(b => b.BookingDate)
                   .IsRequired();

            builder.Property(b => b.StartTime)
                   .IsRequired();

            builder.Property(b => b.EndTime)
                   .IsRequired();

            // Cấu hình trạng thái booking: chuyển enum sang string để dễ đọc trong DB
            builder.Property(b => b.Status)
                   .IsRequired()
                   .HasConversion(
                        v => v.ToString(),
                        v => (BookingStatus)Enum.Parse(typeof(BookingStatus), v))
                   .HasMaxLength(50);

            // Cấu hình mối quan hệ với Facility
            builder.HasOne(b => b.Facility)
                   .WithMany() // Nếu không cần navigation property ngược, hoặc WithMany(f => f.Bookings) nếu có
                   .HasForeignKey(b => b.FacilityId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình mối quan hệ với User
            builder.HasOne(b => b.User)
                   .WithMany() // Tương tự, hoặc WithMany(u => u.Bookings) nếu User có navigation property Bookings
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
