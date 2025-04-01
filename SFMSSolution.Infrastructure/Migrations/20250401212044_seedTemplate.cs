using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("8db81bae-3546-4a9a-a0d5-e63100128f6b"));

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("ad3a9780-a46e-4a53-9bb7-84ea3ae472f4"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1134));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1136));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1137));

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Body", "CreatedBy", "CreatedDate", "Subject", "TemplateName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("abcd1234-abcd-1234-abcd-1234567890ab"), "\r\n                            <p>Xin chào {{UserName}},</p>\r\n                            <p>Bạn đã đăng ký tài khoản thành công.</p>\r\n                            <p>Chúc bạn có những trải nghiệm tuyệt vời cùng chúng tôi!</p>\r\n                            <p>Trân trọng,<br>Sport Facility Management Team</p>", null, new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1333), "Chào mừng đến với 3AT Sport!", "Xác nhận đăng ký thành công", null, null },
                    { new Guid("dcba4321-dcba-4321-dcba-0987654321ef"), "\r\n                            <p>Xin chào {{UserName}},</p>\r\n                            <p>Mã OTP của bạn để đặt lại mật khẩu là: <strong>{{OTP}}</strong></p>\r\n                            <p>Mã có hiệu lực trong vòng 2 phút.</p>\r\n                            <p>Nếu bạn không yêu cầu thao tác này, vui lòng bỏ qua email.</p>\r\n                            <p>Trân trọng,<br>Sport Facility Management Team</p>", null, new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1336), "Xác nhận mã OTP đặt lại mật khẩu", "OTPVerification", null, null },
                    { new Guid("efef5678-efef-5678-efef-1234567890cd"), "\r\n                            <p>Xin chào {{UserName}},</p>\r\n                            <p>Đơn đặt sân của bạn tại {{FacilityName}} vào lúc {{BookingTime}} đã được xác nhận.</p>\r\n                            <p>Chi tiết:</p>\r\n                            <ul>\r\n                                <li>Sân: {{FacilityName}}</li>\r\n                                <li>Thời gian: {{BookingTime}}</li>\r\n                                <li>Giá: {{Price}}</li>\r\n                            </ul>\r\n                             <p>Trân trọng,<br>Sport Facility Management Team</p>", null, new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1334), "Xác nhận đặt sân thành công!", "Xác nhận đặt sân thành công", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1171));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1169));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1296));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1293));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1299));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1295));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1302));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1301));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1298));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1303));

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "BasePrice", "CreatedBy", "CreatedDate", "FacilityType", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1f73169f-627c-4f63-abd0-09ed00604e24"), 400000m, null, new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1202), "Sân bóng đá", null, null },
                    { new Guid("bd7b8d5b-efe2-443e-9600-9458d8d83b29"), 200000m, null, new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1204), "Sân cầu lông", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "f5129493-0902-420a-82b1-58fb2126807f");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"));

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("1f73169f-627c-4f63-abd0-09ed00604e24"));

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("bd7b8d5b-efe2-443e-9600-9458d8d83b29"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2503));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2506));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2507));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2577));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2541));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2634));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2636));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2632));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2639));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2638));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2635));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "BasePrice", "CreatedBy", "CreatedDate", "FacilityType", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("8db81bae-3546-4a9a-a0d5-e63100128f6b"), 400000m, null, new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2601), "Sân bóng đá", null, null },
                    { new Guid("ad3a9780-a46e-4a53-9bb7-84ea3ae472f4"), 200000m, null, new DateTime(2025, 4, 1, 21, 18, 44, 816, DateTimeKind.Utc).AddTicks(2602), "Sân cầu lông", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "291309f0-182b-4852-ad76-46f993f15adc");
        }
    }
}
