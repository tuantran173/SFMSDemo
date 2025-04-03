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
            builder.Property(ft => ft.StartDate)
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(ft => ft.EndDate)
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(f => f.Status)
                  .IsRequired()
                  .HasDefaultValue(SlotStatus.Available);

            builder.HasOne(ft => ft.Facility)
                   .WithMany()
                   .HasForeignKey(ft => ft.FacilityId)
                   .OnDelete(DeleteBehavior.Cascade);
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
