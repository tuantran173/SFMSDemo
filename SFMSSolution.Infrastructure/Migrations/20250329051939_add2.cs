using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("479b7b44-dccb-4050-a358-52055355efbb"));

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("fbfbf7b5-32bd-4656-9b33-446798ddb368"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4151));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4156));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4159));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4528));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4522));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4707));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4701));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4712));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4704));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4717));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4709));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4719));

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "BasePrice", "CategoryId", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1db4d84f-7147-4f45-80d0-bc6595755a7a"), 200000m, new Guid("b1234567-1234-1234-1234-1234567890bc"), null, new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4632), null, null },
                    { new Guid("86edac8f-9c65-4068-8116-ee2d4b035336"), 400000m, new Guid("a1234567-1234-1234-1234-1234567890ab"), null, new DateTime(2025, 3, 29, 5, 19, 37, 568, DateTimeKind.Utc).AddTicks(4628), null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("1db4d84f-7147-4f45-80d0-bc6595755a7a"));

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("86edac8f-9c65-4068-8116-ee2d4b035336"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6256));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6259));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6261));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6559));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6555));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6686));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6691));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6683));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6696));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6694));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6689));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6699));

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "BasePrice", "CategoryId", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("479b7b44-dccb-4050-a358-52055355efbb"), 200000m, new Guid("b1234567-1234-1234-1234-1234567890bc"), null, new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6632), null, null },
                    { new Guid("fbfbf7b5-32bd-4656-9b33-446798ddb368"), 400000m, new Guid("a1234567-1234-1234-1234-1234567890ab"), null, new DateTime(2025, 3, 29, 5, 16, 55, 491, DateTimeKind.Utc).AddTicks(6629), null, null }
                });
        }
    }
}
