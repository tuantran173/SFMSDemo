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
    public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(f => f.Location)
                   .HasMaxLength(255);

            builder.Property(f => f.Description)
                   .HasMaxLength(500);

            builder.Property(f => f.Capacity)
                   .HasMaxLength(50);

            builder.Property(f => f.Images)
                   .HasMaxLength(500);

            builder.Property(f => f.Status)
                   .IsRequired()
                   .HasMaxLength(50);

            // Giá: bắt buộc, kiểu decimal với precision 18,2
            builder.Property(f => f.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(f => f.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Ngày cập nhật: có thể null
            builder.Property(f => f.UpdatedDate);
        }
    }
}
