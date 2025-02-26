using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMSSolution.Domain.Entities;

namespace SFMSSolution.Infrastructure.Database.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Composite key: UserId và RoleId
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            // Quan hệ với User: 1 User có nhiều UserRole
            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.UserRoles)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ với Role: nếu Role không có navigation property cho UserRole, dùng WithMany() không tham chiếu
            builder.HasOne(ur => ur.Role)
                   .WithMany()  // Hoặc .WithMany(r => r.UserRoles) nếu bạn muốn thêm property UserRoles trong Role
                   .HasForeignKey(ur => ur.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
