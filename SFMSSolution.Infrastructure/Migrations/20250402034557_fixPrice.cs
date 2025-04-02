using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("1f73169f-627c-4f63-abd0-09ed00604e24"));

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("bd7b8d5b-efe2-443e-9600-9458d8d83b29"));

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                table: "FacilityPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilityType",
                table: "FacilityPrices",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceId",
                table: "FacilityPrices",
                type: "char(36)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9501));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9503));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9504));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9676));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9680));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9678));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9539));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9536));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9603));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9599));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9606));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9601));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9609));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9608));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9605));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9610));

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "BasePrice", "CreatedBy", "CreatedDate", "FacilityType", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("5ffc9963-2369-4ea1-b32e-6c26bcd6c989"), 400000m, null, new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9567), "Sân bóng đá", null, null },
                    { new Guid("987d5722-35f5-4021-8726-44019e206863"), 200000m, null, new DateTime(2025, 4, 2, 3, 45, 56, 534, DateTimeKind.Utc).AddTicks(9568), "Sân cầu lông", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "6780b403-be61-4d2c-963d-9553029dd38c");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityPrices_PriceId",
                table: "FacilityPrices",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityPrices_Prices_PriceId",
                table: "FacilityPrices",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacilityPrices_Prices_PriceId",
                table: "FacilityPrices");

            migrationBuilder.DropIndex(
                name: "IX_FacilityPrices_PriceId",
                table: "FacilityPrices");

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("5ffc9963-2369-4ea1-b32e-6c26bcd6c989"));

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("987d5722-35f5-4021-8726-44019e206863"));

            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "FacilityPrices");

            migrationBuilder.DropColumn(
                name: "FacilityType",
                table: "FacilityPrices");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "FacilityPrices");

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

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1333));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1336));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 1, 21, 20, 43, 766, DateTimeKind.Utc).AddTicks(1334));

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
    }
}
