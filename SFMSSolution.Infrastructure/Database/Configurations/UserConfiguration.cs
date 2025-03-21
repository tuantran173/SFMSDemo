using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;
using System;

namespace SFMSSolution.Infrastructure.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Phone)
                   .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.Address)
                   .HasMaxLength(500);
            builder.Property(u => u.AvatarUrl)
                   .HasMaxLength(500);
            builder.Property(u => u.Status)
                   .IsRequired();

            // Khai báo thuộc tính RoleId
            builder.Property(u => u.RoleId)
                   .IsRequired();

            // Cấu hình mối quan hệ với bảng Role
            builder.HasOne(u => u.Role)
                   .WithMany() // Nếu Role không có danh sách Users thì để trống
                   .HasForeignKey(u => u.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.UserTokens)
                   .WithOne(ut => ut.User)
                   .HasForeignKey(ut => ut.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Xóa User thì xóa luôn Token

            builder.Property(u => u.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.UpdatedDate)
                   .IsRequired(false);
        }
    }
}
