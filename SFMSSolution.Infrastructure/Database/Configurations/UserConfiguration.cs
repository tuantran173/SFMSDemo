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
            // Khóa chính
            builder.HasKey(u => u.Id);

            // FullName: bắt buộc, tối đa 100 ký tự
            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            // Email: bắt buộc, tối đa 255 ký tự, tạo index duy nhất
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.HasIndex(u => u.Email).IsUnique();

            // Phone: tùy chọn, tối đa 50 ký tự
            builder.Property(u => u.Phone)
                   .HasMaxLength(50);

            // PasswordHash: bắt buộc
            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            // Status: bắt buộc
            builder.Property(u => u.Status)
                   .IsRequired();

            // RefreshToken: tối đa 500 ký tự (nếu có)
            builder.Property(u => u.RefreshToken)
                   .HasMaxLength(500);

            // ResetPasswordToken: tối đa 500 ký tự (nếu có)
            builder.Property(u => u.ResetPasswordToken)
                   .HasMaxLength(500);

            // CreatedDate: mặc định GETUTCDATE()
            builder.Property(u => u.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            // UpdatedDate: có thể null
            builder.Property(u => u.UpdatedDate);
        }
    }
}
