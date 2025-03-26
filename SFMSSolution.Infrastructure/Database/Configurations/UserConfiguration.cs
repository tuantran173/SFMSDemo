using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;

namespace SFMSSolution.Infrastructure.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Identity tự động đặt tên bảng là "AspNetUsers", nhưng bạn có thể ghi đè nếu muốn
            builder.ToTable("Users");

            // Cấu hình các thuộc tính tùy chỉnh
            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            // Email đã được IdentityUser quản lý, nhưng bạn có thể thêm ràng buộc bổ sung
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.HasIndex(u => u.Email).IsUnique();

            // Dùng PhoneNumber của IdentityUser thay vì Phone
            builder.Property(u => u.PhoneNumber) // Thay u.Phone bằng u.PhoneNumber
                   .HasMaxLength(50);


            builder.Property(u => u.Address)
                   .HasMaxLength(500);

            builder.Property(u => u.AvatarUrl)
                   .HasMaxLength(500);

            builder.Property(u => u.Status)
                   .IsRequired()
                   .HasDefaultValue(EntityStatus.Active);

            builder.Property(u => u.Gender)
                   .IsRequired();

            builder.Property(u => u.Birthday)
                   .IsRequired(false); // Nullable



            //    // Nếu User kế thừa BaseEntity có CreatedDate và UpdatedDate
            //    builder.Property(u => u.CreatedDate)
            //           .HasDefaultValueSql("GETUTCDATE()");

            //    builder.Property(u => u.UpdatedDate)
            //           .IsRequired(false);
            //}
        }
    }
}