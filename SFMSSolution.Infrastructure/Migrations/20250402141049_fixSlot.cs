using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacilityType",
                table: "FacilityPrices");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "FacilityTimeSlots",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "FacilityTimeSlots",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "FacilityId",
                table: "FacilityPrices",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9162));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9164));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9165));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9259));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9262));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9261));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9195));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9192));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9225), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9222), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9228), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9224), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9231), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9229), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9227), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 4, 2, 14, 10, 45, 954, DateTimeKind.Utc).AddTicks(9232), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "668b8c0a-b12e-4947-bbba-9aac09758ac9");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityPrices_FacilityId",
                table: "FacilityPrices",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityPrices_Facilities_FacilityId",
                table: "FacilityPrices",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacilityPrices_Facilities_FacilityId",
                table: "FacilityPrices");

            migrationBuilder.DropIndex(
                name: "IX_FacilityPrices_FacilityId",
                table: "FacilityPrices");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "FacilityTimeSlots");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "FacilityTimeSlots");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "FacilityPrices");

            migrationBuilder.AddColumn<string>(
                name: "FacilityType",
                table: "FacilityPrices",
                type: "varchar(100)",
                maxLength: 100,
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
    }
}
