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
    public class SlotDetailConfiguration : IEntityTypeConfiguration<SlotDetail>
    {
        public void Configure(EntityTypeBuilder<SlotDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.FinalPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Note)
                   .HasMaxLength(1000);

            builder.HasIndex(x => new { x.SlotId, x.Date, x.StartTime, x.EndTime })
                   .IsUnique();

            builder.HasOne(x => x.FacilityTimeSlot)
                   .WithMany()
                   .HasForeignKey(x => x.SlotId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedDate).IsRequired();
        }
    }
}
