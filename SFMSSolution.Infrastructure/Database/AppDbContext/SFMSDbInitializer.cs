using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Entities.Base;
using SFMSSolution.Domain.Enums;
using System;
using Role = SFMSSolution.Domain.Entities.Role;

namespace SFMSSolution.Infrastructure.Database.SFMSDbContext
{
    public static class SFMSDbInitializer
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // ========= Seed Roles =========
            // Sử dụng GUID cố định cho các role
            var adminRoleId = new Guid("06324a24-cb26-4452-a3b8-731e1b980e5f");
            var facilityOwnerRoleId = new Guid("dc8b1c2b-91b6-4160-b93e-d5a3b24d8f5d");
            var customerRoleId = new Guid("ca39c26a-406e-424b-bab0-389b6efe38ed");

            var adminRole = new Role
            {
                Id = adminRoleId,
                Name = "Admin",
                RoleCode = "AD",
                Status = EntityStatus.Active,
                CreatedDate = DateTime.UtcNow
            };

            var facilityOwnerRole = new Role
            {
                Id = facilityOwnerRoleId,
                Name = "Facility Owner",
                RoleCode = "FO",
                Status = EntityStatus.Active,
                CreatedDate = DateTime.UtcNow
            };

            var customerRole = new Role
            {
                Id = customerRoleId,
                Name = "Customer",
                RoleCode = "CUS",
                Status = EntityStatus.Active,
                CreatedDate = DateTime.UtcNow
            };

            modelBuilder.Entity<Role>().HasData(adminRole, facilityOwnerRole, customerRole);

            // ========= Seed Users =========
            // Sử dụng GUID cố định cho các user
            var adminUserId = new Guid("a377cd0a-560f-4182-a046-903fa0b04434");
            var ownerUserId = new Guid("17123670-e7fc-4656-932d-1d2525e9c1c0");
            var customerUserId = new Guid("dff0783d-10b7-4aa9-9fdc-364591a7c45b");

            var adminUser = new User
            {
                Id = adminUserId,
                FullName = "Admin",
                Email = "admin@gmail.com",
                Phone = "0974209212",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Gender = Gender.Male,
                Status = EntityStatus.Active,
                Deleted = false,
                CreatedDate = DateTime.UtcNow,
                RefreshToken = string.Empty,
                ResetPasswordToken = string.Empty
            };

            var ownerUser = new User
            {
                Id = ownerUserId,
                FullName = "Facility Owner",
                Email = "owner@gmail.com",
                Phone = "0987654321",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Owner@123"),
                Gender = Gender.Female,
                Status = EntityStatus.Active,
                Deleted = false,
                CreatedDate = DateTime.UtcNow,
                RefreshToken = string.Empty,
                ResetPasswordToken = string.Empty
            };

            var customerUser = new User
            {
                Id = customerUserId,
                FullName = "Customer",
                Email = "customer@gmail.com",
                Phone = "0112233445",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Customer@123"),
                Gender = Gender.Male,
                Status = EntityStatus.Active,
                Deleted = false,
                CreatedDate = DateTime.UtcNow,
                RefreshToken = string.Empty,
                ResetPasswordToken = string.Empty
            };

            modelBuilder.Entity<User>().HasData(adminUser, ownerUser, customerUser);

            // ========= Seed UserRoles =========
            // Liên kết mỗi user với role tương ứng
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = adminUserId, RoleId = adminRoleId },
                new UserRole { UserId = ownerUserId, RoleId = facilityOwnerRoleId },
                new UserRole { UserId = customerUserId, RoleId = customerRoleId }
            );

            // ========= Seed Facilities =========
            var facility1Id = Guid.NewGuid();
            var facility2Id = Guid.NewGuid();
            var facility3Id = Guid.NewGuid();

            var facility1 = new Facility
            {
                Id = facility1Id,
                Name = "Sân bóng",
                Location = "Thôn 3, Thạch Hòa, Thạch Thất, Hà Nội",
                Description = "Sân bóng cỏ tự nhiên với trang thiết bị hiện đại.",
                Capacity = "22",
                Images = "/uploads/facilities/sanh_bong_a.jpg",
                Status = "Available",
                Price = 500m,
                CreatedDate = DateTime.UtcNow
            };

            var facility2 = new Facility
            {
                Id = facility2Id,
                Name = "Sân cầu lông",
                Location = "Thôn 3, Thạch Hòa, Thạch Thất, Hà Nội",
                Description = "Sân cầu lông trong nhà, điều hòa, thích hợp cho giải đấu.",
                Capacity = "4",
                Images = "/uploads/facilities/sanh_tennis_b.jpg",
                Status = "Under Maintenance",
                Price = 300m,
                CreatedDate = DateTime.UtcNow
            };

            var facility3 = new Facility
            {
                Id = facility3Id,
                Name = "Sân Pickleball",
                Location = "Thôn 3, Thạch Hòa, Thạch Thất, Hà Nội",
                Description = "Sân Pickleball hiện đại với trang thiết bị hiện đại.",
                Capacity = "30",
                Images = "/uploads/facilities/phong_gym_c.jpg",
                Status = "Closed",
                Price = 100m,
                CreatedDate = DateTime.UtcNow
            };

            modelBuilder.Entity<Facility>().HasData(facility1, facility2, facility3);

            // ========= Seed Bookings =========
            // Giả sử có 2 booking: Admin đặt sân tại facility1, Customer đặt sân tại facility2
            var booking1 = new Booking
            {
                Id = Guid.NewGuid(),
                BookingDate = DateTime.UtcNow.Date.AddDays(1),
                StartTime = DateTime.UtcNow.Date.AddDays(1).AddHours(9),
                EndTime = DateTime.UtcNow.Date.AddDays(1).AddHours(11),
                FacilityId = facility1Id,
                UserId = adminUserId,
                Status = BookingStatus.Pending,
                CreatedDate = DateTime.UtcNow
            };

            var booking2 = new Booking
            {
                Id = Guid.NewGuid(),
                BookingDate = DateTime.UtcNow.Date.AddDays(1),
                StartTime = DateTime.UtcNow.Date.AddDays(1).AddHours(14),
                EndTime = DateTime.UtcNow.Date.AddDays(1).AddHours(16),
                FacilityId = facility2Id,
                UserId = customerUserId,
                Status = BookingStatus.Confirmed,
                CreatedDate = DateTime.UtcNow
            };

            modelBuilder.Entity<Booking>().HasData(booking1, booking2);
        }
    }
}
