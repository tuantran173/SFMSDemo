using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedBookingTemplateMail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9506));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9507));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9509));

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
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9667));

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Body", "CreatedBy", "CreatedDate", "Subject", "TemplateName", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("e1111111-2222-3333-4444-555566667777"), "\r\n                        <html>\r\n                        <body style='font-family: Arial, sans-serif; color: #333;'>\r\n                            <h2>Xin chào {{OwnerName}},</h2>\r\n                            <p>Bạn vừa nhận được một <strong>yêu cầu đặt sân</strong> từ khách hàng <strong>{{CustomerName}}</strong>.</p>\r\n                            <table style='width: 100%; margin: 20px 0;'>\r\n                                <tr><td><strong>Sân:</strong></td><td>{{FacilityName}}</td></tr>\r\n                                <tr><td><strong>Thời gian:</strong></td><td>{{BookingTime}}</td></tr>\r\n                                <tr><td><strong>Giá:</strong></td><td>{{Price}}</td></tr>\r\n                            </table>\r\n                            <p>Vui lòng xác nhận hoặc từ chối yêu cầu:</p>\r\n                            <p>\r\n                                <a href='{{ConfirmLink}}' style='padding: 10px 20px; background-color: #28a745; color: white; text-decoration: none;'>Xác nhận</a>\r\n                                &nbsp;\r\n                                <a href='{{RejectLink}}' style='padding: 10px 20px; background-color: #dc3545; color: white; text-decoration: none;'>Từ chối</a>\r\n                            </p>\r\n                            <p>Trân trọng,<br>SFMS Team</p>\r\n                        </body>\r\n                        </html>", null, new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9650), "Yêu cầu xác nhận đặt sân", "BookingRequestOwner", null, null });

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9581));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9578));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9612));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9609));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9614));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9610));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9618));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9616));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9613));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 43, 44, 882, DateTimeKind.Utc).AddTicks(9619));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "96467c8a-903d-4ee1-a331-e4c978b44250");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-2222-3333-4444-555566667777"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(887));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(889));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(891));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(1007));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(1010));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(1008));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(929));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(926));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(961));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(967));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(963));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(970));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(969));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(966));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 3, 8, 32, 419, DateTimeKind.Utc).AddTicks(972));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "5303a9fd-e054-432c-bf68-1ddf517a5d22");
        }
    }
}
