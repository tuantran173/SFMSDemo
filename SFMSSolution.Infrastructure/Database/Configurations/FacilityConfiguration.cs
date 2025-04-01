using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;

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

            builder.Property(f => f.ImageUrl)
                   .HasMaxLength(500);

            builder.Property(f => f.FacilityType)
                   .IsRequired()
                   .HasMaxLength(100); // vì FacilityType là string
            builder.Property(f => f.Status)
                   .IsRequired()
                   .HasDefaultValue(FacilityStatus.Available);
            builder.Property(f => f.OwnerId)
                   .IsRequired();

            builder.HasOne(f => f.Owner)
                   .WithMany() // nếu bạn thêm ICollection<Facility> trong User thì dùng .WithMany(u => u.Facilities)
                   .HasForeignKey(f => f.OwnerId)
                   .OnDelete(DeleteBehavior.Restrict); // tránh xóa cascade khi xóa user

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
