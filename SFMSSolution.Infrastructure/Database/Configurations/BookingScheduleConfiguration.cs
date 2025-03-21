using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Database.Configurations
{
    public class BookingScheduleConfiguration : IEntityTypeConfiguration<BookingSchedule>
    {
        public void Configure(EntityTypeBuilder<BookingSchedule> builder)
        {
            builder.ToTable("BookingSchedules");

            builder.HasKey(bs => bs.Id);

            builder.Property(bs => bs.Date)
                   .IsRequired();

            builder.Property(bs => bs.StartTime)
                   .IsRequired();

            builder.Property(bs => bs.EndTime)
                   .IsRequired();

            builder.Property(bs => bs.Status)
                   .IsRequired();

            builder.HasOne(bs => bs.Facility)
                   .WithMany()
                   .HasForeignKey(bs => bs.FacilityId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
