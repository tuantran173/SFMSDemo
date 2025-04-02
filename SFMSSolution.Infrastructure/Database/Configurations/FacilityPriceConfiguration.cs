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
    public class FacilityPriceConfiguration : IEntityTypeConfiguration<FacilityPrice>
    {
        public void Configure(EntityTypeBuilder<FacilityPrice> builder)
        {
            builder.ToTable("FacilityPrices");

            builder.HasKey(fp => fp.Id);

            builder.Property(fp => fp.Coefficient)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(fp => fp.BasePrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(fp => fp.FinalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(fp => fp.FacilityType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(fp => fp.FacilityTimeSlot)
                   .WithMany()
                   .HasForeignKey(fp => fp.FacilityTimeSlotId)
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
