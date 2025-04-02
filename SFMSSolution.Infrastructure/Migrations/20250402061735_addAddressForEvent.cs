using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addAddressForEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Events",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5488));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5490));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5491));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5625));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5624));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5555));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5519));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5590));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5586));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5593));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5588));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5596));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5594));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5591));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 6, 17, 34, 17, DateTimeKind.Utc).AddTicks(5597));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "dda6ab9d-1123-4ea7-9be2-3d4762cabd38");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2348));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2349));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2351));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2465));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2468));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2467));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2387));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2385));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2425));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2422));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2428));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2424));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2431));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2429));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2426));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 4, 23, 11, 576, DateTimeKind.Utc).AddTicks(2432));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "4a8d3813-4a8c-4661-becc-3302630b77ed");
        }
    }
}
