using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;

namespace SFMSSolution.Infrastructure.Database.SFMSDbContext
{
    public static class SFMSDbInitializer
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var ownerId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = ownerId,
                FullName = "Facility Owner",
                Email = "owner@example.com",
                UserName = "owner",
                NormalizedUserName = "OWNER",
                NormalizedEmail = "OWNER@EXAMPLE.COM",
                PasswordHash = "Trantuan_2003", // hoặc để trống nếu chưa dùng auth
                EmailConfirmed = true,
                Status = EntityStatus.Active,
                Gender = Gender.Male,
                Birthday = new DateTime(1990, 1, 1)
            });

            // Khai báo ID cho các Category
            var footballFieldId = Guid.Parse("A1234567-1234-1234-1234-1234567890AB");
            var badmintonCourtId = Guid.Parse("B1234567-1234-1234-1234-1234567890BC");
            var pickleballCourtId = Guid.Parse("C1234567-1234-1234-1234-1234567890CD");

            // Seed dữ liệu cho bảng Categories (Thể loại sân)
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = footballFieldId,
                    Name = "Sân bóng",
                    Description = "Sân bóng đá 5-a-side, 7-a-side, 11-a-side",
                    CreatedDate = DateTime.UtcNow
                },
                new Category
                {
                    Id = badmintonCourtId,
                    Name = "Sân cầu lông",
                    Description = "Sân cầu lông đơn và đôi",
                    CreatedDate = DateTime.UtcNow
                },
                new Category
                {
                    Id = pickleballCourtId,
                    Name = "Sân Pickleball",
                    Description = "Sân Pickleball chuẩn quốc tế",
                    CreatedDate = DateTime.UtcNow
                }
            );

            var facility1Id = Guid.Parse("f34c777a-fa4b-4ed1-bc22-29570a01d7d9");
            var facility2Id = Guid.Parse("9eefd023-7cc3-428f-b96d-3e0430394391");

            modelBuilder.Entity<Facility>().HasData(
                new Facility
                {
                    Id = facility1Id,
                    Name = "Football Field 5-a-side",
                    Address = "Thạch Thất, Hòa Lạc",
                    Description = "Sân bóng đá 5 người",
                    ImageUrl = "image1.jpg",
                    FacilityType = "Football",
                    OwnerId = ownerId,
                    Status = FacilityStatus.Available,
                    CreatedDate = DateTime.UtcNow
                },
                new Facility
                {
                    Id = facility2Id,
                    Name = "Badminton Court 1",
                    Address = "Thạch Thất, Hòa Lạc",
                    Description = "Sân cầu lông đơn/đôi",
                    ImageUrl = "image2.jpg",
                    FacilityType = "Badminton",
                    OwnerId = ownerId,
                    Status = FacilityStatus.Available,
                    CreatedDate = DateTime.UtcNow
                }
            );


            var timeslot1Id = Guid.Parse("1b05b57c-6d02-4c06-b0b5-a96139825346");
            var timeslot2Id = Guid.Parse("907b662c-5a2c-4a90-b96b-81b603b27e57");
            var timeslot3Id = Guid.Parse("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17");
            var timeslot4Id = Guid.Parse("d75d092a-7da6-4cc3-88c9-69ac5c82652c");
            var timeslot5Id = Guid.Parse("1b7ea0d1-c743-47d7-b3f1-02860dbd9806");
            var timeslot6Id = Guid.Parse("bb9299e1-518a-4730-9797-6ec37c5dd03f");
            var timeslot7Id = Guid.Parse("b03366d1-b1cc-4c0e-8e61-6fff1651755d");
            var timeslot8Id = Guid.Parse("ffa61f3b-58a0-4881-ae97-61332f81fc4f");
            var timeslot9Id = Guid.Parse("d2fa5bfd-9d4d-4d6e-ac00-92c6d056a1e5");
            var timeslot10Id = Guid.Parse("3784a1ea-e255-4156-bee8-296431f27dfd");

            // Seed dữ liệu cho bảng FacilityTimeSlot (TimeSlot)
            modelBuilder.Entity<FacilityTimeSlot>().HasData(
                // TimeSlots for Facility 1 (Football Field 5-a-side)
                new FacilityTimeSlot
                {
                    Id = timeslot1Id,
                    FacilityId = facility1Id,
                    StartTime = new TimeSpan(8, 0, 0),    // 8:00 AM
                    EndTime = new TimeSpan(9, 30, 0),     // 9:30 AM
                    CreatedDate = DateTime.UtcNow
                },
                new FacilityTimeSlot
                {
                    Id = timeslot2Id,
                    FacilityId = facility1Id,
                    StartTime = new TimeSpan(9, 30, 0),   // 9:30 AM
                    EndTime = new TimeSpan(11, 0, 0),     // 11:00 AM
                    CreatedDate = DateTime.UtcNow
                },
                new FacilityTimeSlot
                {
                    Id = timeslot3Id,
                    FacilityId = facility1Id,
                    StartTime = new TimeSpan(11, 0, 0),   // 11:00 AM
                    EndTime = new TimeSpan(12, 30, 0),    // 12:30 PM
                    CreatedDate = DateTime.UtcNow
                },
                new FacilityTimeSlot
                {
                    Id = timeslot4Id,
                    FacilityId = facility1Id,
                    StartTime = new TimeSpan(14, 0, 0),   // 2:00 PM
                    EndTime = new TimeSpan(15, 30, 0),    // 3:30 PM
                    CreatedDate = DateTime.UtcNow
                },
                new FacilityTimeSlot
                {
                    Id = timeslot5Id,
                    FacilityId = facility1Id,
                    StartTime = new TimeSpan(15, 30, 0),  // 3:30 PM
                    EndTime = new TimeSpan(17, 0, 0),     // 5:00 PM
                    CreatedDate = DateTime.UtcNow
                },

                // TimeSlots for Facility 2 (Badminton Court 1)
                new FacilityTimeSlot
                {
                    Id = timeslot6Id,
                    FacilityId = facility2Id,
                    StartTime = new TimeSpan(8, 0, 0),    // 8:00 AM
                    EndTime = new TimeSpan(9, 30, 0),     // 9:30 AM
                    CreatedDate = DateTime.UtcNow
                },
                new FacilityTimeSlot
                {
                    Id = timeslot7Id,
                    FacilityId = facility2Id,
                    StartTime = new TimeSpan(9, 30, 0),   // 9:30 AM
                    EndTime = new TimeSpan(11, 0, 0),     // 11:00 AM
                    CreatedDate = DateTime.UtcNow
                },
                new FacilityTimeSlot
                {
                    Id = timeslot8Id,
                    FacilityId = facility2Id,
                    StartTime = new TimeSpan(11, 0, 0),   // 11:00 AM
                    EndTime = new TimeSpan(12, 30, 0),    // 12:30 PM
                    CreatedDate = DateTime.UtcNow
                });


            var emailTemplate1Id = Guid.Parse("abcd1234-abcd-1234-abcd-1234567890ab");
            var emailTemplate2Id = Guid.Parse("efef5678-efef-5678-efef-1234567890cd"); // ✅ hợp lệ
            var emailTemplate3Id = Guid.Parse("dcba4321-dcba-4321-dcba-0987654321ef"); // ✅ hợp lệ
            var bookingRequestOwnerTemplateId = Guid.Parse("e1111111-2222-3333-4444-555566667777");

            modelBuilder.Entity<EmailTemplate>().HasData(
                new EmailTemplate
                {
                    Id = bookingRequestOwnerTemplateId,
                    TemplateName = "BookingRequestOwner",
                    Subject = "Yêu cầu xác nhận đặt sân",
                    Body = @"
                        <html>
                        <body style='font-family: Arial, sans-serif; color: #333;'>
                            <h2>Xin chào {{OwnerName}},</h2>
                            <p>Bạn vừa nhận được một <strong>yêu cầu đặt sân</strong> từ khách hàng <strong>{{CustomerName}}</strong>.</p>
                            <table style='width: 100%; margin: 20px 0;'>
                                <tr><td><strong>Sân:</strong></td><td>{{FacilityName}}</td></tr>
                                <tr><td><strong>Thời gian:</strong></td><td>{{BookingTime}}</td></tr>
                                <tr><td><strong>Giá:</strong></td><td>{{Price}}</td></tr>
                            </table>
                            <p>Vui lòng xác nhận hoặc từ chối yêu cầu:</p>
                            <p>
                                <a href='{{ConfirmLink}}' style='padding: 10px 20px; background-color: #28a745; color: white; text-decoration: none;'>Xác nhận</a>
                                &nbsp;
                                <a href='{{RejectLink}}' style='padding: 10px 20px; background-color: #dc3545; color: white; text-decoration: none;'>Từ chối</a>
                            </p>
                            <p>Trân trọng,<br>SFMS Team</p>
                        </body>
                        </html>",
                    CreatedDate = DateTime.UtcNow
                }
            );
            modelBuilder.Entity<EmailTemplate>().HasData(
                new EmailTemplate
                {
                    Id = emailTemplate1Id,
                    TemplateName = "Xác nhận đăng ký thành công",
                    Subject = "Chào mừng đến với 3AT Sport!",
                    Body = @"
                            <p>Xin chào {{UserName}},</p>
                            <p>Bạn đã đăng ký tài khoản thành công.</p>
                            <p>Chúc bạn có những trải nghiệm tuyệt vời cùng chúng tôi!</p>
                            <p>Trân trọng,<br>Sport Facility Management Team</p>",
                    CreatedDate = DateTime.UtcNow
                },
                new EmailTemplate
                {
                    Id = emailTemplate2Id,
                    TemplateName = "Xác nhận đặt sân thành công",
                    Subject = "Xác nhận đặt sân thành công!",
                    Body = @"
                            <p>Xin chào {{UserName}},</p>
                            <p>Đơn đặt sân của bạn tại {{FacilityName}} vào lúc {{BookingTime}} đã được xác nhận.</p>
                            <p>Chi tiết:</p>
                            <ul>
                                <li>Sân: {{FacilityName}}</li>
                                <li>Thời gian: {{BookingTime}}</li>
                                <li>Giá: {{Price}}</li>
                            </ul>
                             <p>Trân trọng,<br>Sport Facility Management Team</p>",
                    CreatedDate = DateTime.UtcNow
                },
                new EmailTemplate
                {
                    Id = emailTemplate3Id,
                    TemplateName = "OTPVerification",
                    Subject = "Xác nhận mã OTP đặt lại mật khẩu",
                    Body = @"
                            <p>Xin chào {{UserName}},</p>
                            <p>Mã OTP của bạn để đặt lại mật khẩu là: <strong>{{OTP}}</strong></p>
                            <p>Mã có hiệu lực trong vòng 2 phút.</p>
                            <p>Nếu bạn không yêu cầu thao tác này, vui lòng bỏ qua email.</p>
                            <p>Trân trọng,<br>Sport Facility Management Team</p>",
                    CreatedDate = DateTime.UtcNow
                }
            );
        }
    }
}
