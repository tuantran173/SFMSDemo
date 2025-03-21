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

            builder.Property(fp => fp.FinalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(fp => fp.FacilityTimeSlot)
                   .WithMany()
                   .HasForeignKey(fp => fp.FacilityTimeSlotId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
