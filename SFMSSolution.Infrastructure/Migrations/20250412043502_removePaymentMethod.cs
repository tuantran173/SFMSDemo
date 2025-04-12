using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removePaymentMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 12, 4, 35, 0, 244, DateTimeKind.Utc).AddTicks(9702));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 12, 4, 35, 0, 244, DateTimeKind.Utc).AddTicks(9705));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-2222-3333-4444-555566667777"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 12, 4, 35, 0, 244, DateTimeKind.Utc).AddTicks(9683));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 12, 4, 35, 0, 244, DateTimeKind.Utc).AddTicks(9704));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "32529e58-486e-4405-8bd1-e0cc6c48b441");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Bookings",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 8, 53, 55, 627, DateTimeKind.Utc).AddTicks(8758));

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
    }
}
