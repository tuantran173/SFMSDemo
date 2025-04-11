using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedBookingTemplateMail2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"));

            migrationBuilder.DeleteData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"));

            migrationBuilder.DeleteData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"));

            migrationBuilder.DeleteData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"));

            migrationBuilder.DeleteData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"));

            migrationBuilder.DeleteData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"));

            migrationBuilder.DeleteData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"));

            migrationBuilder.DeleteData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"));

            migrationBuilder.DeleteData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"));

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"));

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("a1234567-1234-1234-1234-1234567890ab"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9506), "Sân bóng đá 5-a-side, 7-a-side, 11-a-side", "Sân bóng", null, null },
                    { new Guid("b1234567-1234-1234-1234-1234567890bc"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9507), "Sân cầu lông đơn và đôi", "Sân cầu lông", null, null },
                    { new Guid("c1234567-1234-1234-1234-1234567890cd"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9509), "Sân Pickleball chuẩn quốc tế", "Sân Pickleball", null, null }
                });

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9665));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9668));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-2222-3333-4444-555566667777"),
                columns: new[] { "Body", "CreatedDate" },
                values: new object[] { "\r\n                        <html>\r\n                        <body style='font-family: Arial, sans-serif; color: #333;'>\r\n                            <h2>Xin chào {{OwnerName}},</h2>\r\n                            <p>Bạn vừa nhận được một <strong>yêu cầu đặt sân</strong> từ khách hàng <strong>{{CustomerName}}</strong>.</p>\r\n                            <table style='width: 100%; margin: 20px 0;'>\r\n                                <tr><td><strong>Sân:</strong></td><td>{{FacilityName}}</td></tr>\r\n                                <tr><td><strong>Thời gian:</strong></td><td>{{BookingTime}}</td></tr>\r\n                                <tr><td><strong>Giá:</strong></td><td>{{Price}}</td></tr>\r\n                            </table>\r\n                            <p>Vui lòng xác nhận hoặc từ chối yêu cầu:</p>\r\n                            <p>\r\n                                <a href='{{ConfirmLink}}' style='padding: 10px 20px; background-color: #28a745; color: white; text-decoration: none;'>Xác nhận</a>\r\n                                &nbsp;\r\n                                <a href='{{RejectLink}}' style='padding: 10px 20px; background-color: #dc3545; color: white; text-decoration: none;'>Từ chối</a>\r\n                            </p>\r\n                            <p>Trân trọng,<br>SFMS Team</p>\r\n                        </body>\r\n                        </html>", new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9650) });

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9667));

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedDate", "Description", "FacilityType", "ImageUrl", "Name", "OwnerId", "Status", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), "Thạch Thất, Hòa Lạc", null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9581), "Sân cầu lông đơn/đôi", "Badminton", "image2.jpg", "Badminton Court 1", new Guid("11111111-1111-1111-1111-111111111111"), 1, null, null },
                    { new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), "Thạch Thất, Hòa Lạc", null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9578), "Sân bóng đá 5 người", "Football", "image1.jpg", "Football Field 5-a-side", new Guid("11111111-1111-1111-1111-111111111111"), 1, null, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "96467c8a-903d-4ee1-a331-e4c978b44250");

            migrationBuilder.InsertData(
                table: "FacilityTimeSlots",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EndDate", "EndTime", "FacilityId", "FacilityId1", "IsWeekend", "StartDate", "StartTime", "Status", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9612), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 30, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), 0, null, null },
                    { new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9609), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 30, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), 0, null, null },
                    { new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9614), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 17, 0, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 30, 0, 0), 0, null, null },
                    { new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9610), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 30, 0, 0), 0, null, null },
                    { new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9618), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 30, 0, 0), 0, null, null },
                    { new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9616), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 30, 0, 0), new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), 0, null, null },
                    { new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9613), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 30, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 0, 0, 0), 0, null, null },
                    { new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"), null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9619), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 12, 30, 0, 0), new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), 0, null, null }
                });
        }
    }
}
