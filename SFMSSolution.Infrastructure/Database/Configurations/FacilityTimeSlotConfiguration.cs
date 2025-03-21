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
    public class FacilityTimeSlotConfiguration : IEntityTypeConfiguration<FacilityTimeSlot>
    {
        public void Configure(EntityTypeBuilder<FacilityTimeSlot> builder)
        {
            builder.ToTable("FacilityTimeSlots");

            builder.HasKey(ft => ft.Id);

            builder.Property(ft => ft.StartTime)
                   .IsRequired();

            builder.Property(ft => ft.EndTime)
                   .IsRequired();

            builder.Property(ft => ft.IsWeekend)
                   .IsRequired();

            builder.HasOne(ft => ft.Facility)
                   .WithMany()
                   .HasForeignKey(ft => ft.FacilityId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
