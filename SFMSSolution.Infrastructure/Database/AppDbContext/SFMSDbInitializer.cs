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
                            <p>Bạn vừa nhận được một <strong>yêu cầu đặt sân</strong> từ khách hàng:</p>
                            <ul>
                                <li><strong>Họ tên:</strong> {{CustomerName}}</li>
                                <li><strong>Số điện thoại:</strong> {{CustomerPhone}}</li>
                                <li><strong>Email:</strong> {{CustomerEmail}}</li>
                            </ul>
                            <p><strong>Thông tin sân:</strong></p>
                            <ul>
                                <li><strong>Sân:</strong> {{FacilityName}}</li>
                                <li><strong>Địa chỉ:</strong> {{FacilityAddress}}</li>
                                <li><strong>Ngày:</strong> {{BookingDate}}</li>
                                <li><strong>Giờ:</strong> {{BookingTime}}</li>
                                <li><strong>Giá:</strong> {{Price}}</li>
                                <li><strong>Phương thức thanh toán:</strong> {{PaymentMethod}}</li>
                                <li><strong>Ghi chú:</strong> {{Note}}</li>
                                <li><strong>Ảnh chuyển khoản:</strong><br/>
                                    <img src='{{PayUrl}}' alt='Ảnh chuyển khoản' style='max-width: 400px; border: 1px solid #ccc;' />
                                </li>
                            </ul>

                            <p>Vui lòng xác nhận hoặc từ chối yêu cầu:</p>
                            <p>
                                <a href='{{ConfirmLink}}' style='padding: 10px 20px; background-color: #28a745; color: white; text-decoration: none;'>Xác nhận</a>
                                &nbsp;
                                <a href='{{RejectLink}}' style='padding: 10px 20px; background-color: #dc3545; color: white; text-decoration: none;'>Từ chối</a>
                            </p>

                            <p>Trân trọng,<br>Sport Facility Management Team</p>
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
