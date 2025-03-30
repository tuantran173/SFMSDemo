using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFMSSolution.Domain.Entities;

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

            builder.Property(f => f.Address)
                   .HasMaxLength(255);

            builder.Property(f => f.Description)
                   .HasMaxLength(500);

            builder.Property(f => f.Capacity)
                   .HasMaxLength(50);

            builder.Property(f => f.ImageUrl)
                   .HasMaxLength(500);

            builder.HasOne(f => f.Category)
                    .WithMany()
                    .HasForeignKey(f => f.CategoryId);

            builder.Property(f => f.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Ngày cập nhật: có thể null
            builder.Property(f => f.UpdatedDate);
        }
    }
}
