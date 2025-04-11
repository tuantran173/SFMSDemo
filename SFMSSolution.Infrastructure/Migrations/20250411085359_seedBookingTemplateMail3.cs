using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedBookingTemplateMail3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceImageUrl",
                table: "FacilityPrices");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 8, 53, 55, 627, DateTimeKind.Utc).AddTicks(8777));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 8, 53, 55, 627, DateTimeKind.Utc).AddTicks(8839));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-2222-3333-4444-555566667777"),
                columns: new[] { "Body", "CreatedDate" },
                values: new object[] { "\r\n                        <html>\r\n                        <body style='font-family: Arial, sans-serif; color: #333;'>\r\n                            <h2>Xin chào {{OwnerName}},</h2>\r\n                            <p>Bạn vừa nhận được một <strong>yêu cầu đặt sân</strong> từ khách hàng:</p>\r\n                            <ul>\r\n                                <li><strong>Họ tên:</strong> {{CustomerName}}</li>\r\n                                <li><strong>Số điện thoại:</strong> {{CustomerPhone}}</li>\r\n                                <li><strong>Email:</strong> {{CustomerEmail}}</li>\r\n                            </ul>\r\n                            <p><strong>Thông tin sân:</strong></p>\r\n                            <ul>\r\n                                <li><strong>Sân:</strong> {{FacilityName}}</li>\r\n                                <li><strong>Địa chỉ:</strong> {{FacilityAddress}}</li>\r\n                                <li><strong>Ngày:</strong> {{BookingDate}}</li>\r\n                                <li><strong>Giờ:</strong> {{BookingTime}}</li>\r\n                                <li><strong>Giá:</strong> {{Price}}</li>\r\n                                <li><strong>Phương thức thanh toán:</strong> {{PaymentMethod}}</li>\r\n                                <li><strong>Ghi chú:</strong> {{Note}}</li>\r\n                                <li><strong>Ảnh chuyển khoản:</strong><br/>\r\n                                    <img src='{{PayUrl}}' alt='Ảnh chuyển khoản' style='max-width: 400px; border: 1px solid #ccc;' />\r\n                                </li>\r\n                            </ul>\r\n\r\n                            <p>Vui lòng xác nhận hoặc từ chối yêu cầu:</p>\r\n                            <p>\r\n                                <a href='{{ConfirmLink}}' style='padding: 10px 20px; background-color: #28a745; color: white; text-decoration: none;'>Xác nhận</a>\r\n                                &nbsp;\r\n                                <a href='{{RejectLink}}' style='padding: 10px 20px; background-color: #dc3545; color: white; text-decoration: none;'>Từ chối</a>\r\n                            </p>\r\n\r\n                            <p>Trân trọng,<br>Sport Facility Management Team</p>\r\n                        </body>\r\n                        </html>", new DateTime(2025, 4, 11, 8, 53, 55, 627, DateTimeKind.Utc).AddTicks(8758) });

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 8, 53, 55, 627, DateTimeKind.Utc).AddTicks(8838));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "a440680d-dc6a-4cf4-875d-ca7b563bece7");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriceImageUrl",
                table: "FacilityPrices",
                type: "longtext",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 4, 16, 47, 97, DateTimeKind.Utc).AddTicks(2506));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 4, 16, 47, 97, DateTimeKind.Utc).AddTicks(2509));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-2222-3333-4444-555566667777"),
                columns: new[] { "Body", "CreatedDate" },
                values: new object[] { "\r\n                        <html>\r\n                        <body style='font-family: Arial, sans-serif; color: #333;'>\r\n                            <h2>Xin chào {{OwnerName}},</h2>\r\n                            <p>Bạn vừa nhận được một <strong>yêu cầu đặt sân</strong> từ khách hàng:</p>\r\n                            <ul>\r\n                                <li><strong>Họ tên:</strong> {{CustomerName}}</li>\r\n                                <li><strong>Số điện thoại:</strong> {{CustomerPhone}}</li>\r\n                                <li><strong>Email:</strong> {{CustomerEmail}}</li>\r\n                            </ul>\r\n                            <p><strong>Thông tin sân:</strong></p>\r\n                            <ul>\r\n                                <li><strong>Sân:</strong> {{FacilityName}}</li>\r\n                                <li><strong>Địa chỉ:</strong> {{FacilityAddress}}</li>\r\n                                <li><strong>Ngày:</strong> {{BookingDate}}</li>\r\n                                <li><strong>Giờ:</strong> {{BookingTime}}</li>\r\n                                <li><strong>Giá:</strong> {{Price}}</li>\r\n                                <li><strong>Phương thức thanh toán:</strong> {{PaymentMethod}}</li>\r\n                                <li><strong>Ghi chú:</strong> {{Note}}</li>\r\n                                <li><strong>Ảnh chuyển khoản:</strong><br/>\r\n                                    {{#if PayUrl}}\r\n                                    <img src='{{PayUrl}}' alt='Ảnh chuyển khoản' style='max-width: 400px; border: 1px solid #ccc;' />\r\n                                    {{else}}\r\n                                    Không có\r\n                                    {{/if}}\r\n                                </li>\r\n                            </ul>\r\n\r\n                            <p>Vui lòng xác nhận hoặc từ chối yêu cầu:</p>\r\n                            <p>\r\n                                <a href='{{ConfirmLink}}' style='padding: 10px 20px; background-color: #28a745; color: white; text-decoration: none;'>Xác nhận</a>\r\n                                &nbsp;\r\n                                <a href='{{RejectLink}}' style='padding: 10px 20px; background-color: #dc3545; color: white; text-decoration: none;'>Từ chối</a>\r\n                            </p>\r\n\r\n                            <p>Trân trọng,<br>Sport Facility Management Team</p>\r\n                        </body>\r\n                        </html>", new DateTime(2025, 4, 11, 4, 16, 47, 97, DateTimeKind.Utc).AddTicks(2490) });

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 4, 16, 47, 97, DateTimeKind.Utc).AddTicks(2508));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "06beb911-4399-4df0-8365-61b5b250c11a");
        }
    }
}
