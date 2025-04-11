using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initQRImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "FacilityPrices",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PriceImageUrl",
                table: "FacilityPrices",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Bookings",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5391));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5393));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5500));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5503));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5502));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5429));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5427));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5459));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5456));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5462));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5458));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5465));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5464));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5461));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 2, 58, 26, 791, DateTimeKind.Utc).AddTicks(5466));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "526346f8-6780-428f-9b68-0c0538ee6f30");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "FacilityPrices");

            migrationBuilder.DropColumn(
                name: "PriceImageUrl",
                table: "FacilityPrices");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9704));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9706));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9708));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9829));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9832));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9831));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9748));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9746));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9787));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9783));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9790));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9785));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9793));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9791));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9788));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 11, 1, 52, 1, 479, DateTimeKind.Utc).AddTicks(9794));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "22f1c3d4-c452-403b-9a95-c4d2df894aba");
        }
    }
}
