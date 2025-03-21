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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            // Khóa chính từ BaseEntity (Id)
            builder.HasKey(r => r.Id);

            // Name: bắt buộc, tối đa 100 ký tự
            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // RoleCode: bắt buộc, tối đa 20 ký tự
            builder.Property(r => r.RoleCode)
                   .IsRequired()
                   .HasMaxLength(20);

            // Status: bắt buộc
            builder.Property(r => r.Status)
                   .IsRequired();

            // CreatedDate: có giá trị mặc định khi tạo
            builder.Property(r => r.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            // UpdatedDate: có thể null
            builder.Property(r => r.UpdatedDate);
        }
    }
}
