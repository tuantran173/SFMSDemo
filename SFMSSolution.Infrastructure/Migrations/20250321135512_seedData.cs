using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("241dcce9-51fb-413c-a653-5892d5cac69c"));

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9668b4d4-936f-4a74-866b-915947fb0b33"));

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("1a58a24b-79ee-4a6d-ad48-0ae82d889b17"));

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("75e5fa69-f123-4c8b-9c59-97e1830a01fb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("895d096a-4778-4838-87df-980b2f5f5844"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a37800b7-18a0-4a42-887a-f4868a4dc244"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fab7f104-71e3-481b-bf06-e4df2c5ba999"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2292));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2294));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2296));

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "Capacity", "CategoryId", "CreatedBy", "CreatedDate", "Description", "Images", "Location", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), "4", new Guid("b1234567-1234-1234-1234-1234567890bc"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2404), "Sân cầu lông đơn/đôi", "image2.jpg", "Thạch Thất, Hòa Lạc", "Badminton Court 1", null, null },
                    { new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), "10", new Guid("a1234567-1234-1234-1234-1234567890ab"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2402), "Sân bóng đá 5 người", "image1.jpg", "Thạch Thất, Hòa Lạc", "Football Field 5-a-side", null, null }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "BasePrice", "CategoryId", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("19a0d469-93c9-4158-be0b-daf94107cb46"), 200000m, new Guid("b1234567-1234-1234-1234-1234567890bc"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2488), null, null },
                    { new Guid("957c4719-4bca-4ce0-9b55-ced6573f76d2"), 400000m, new Guid("a1234567-1234-1234-1234-1234567890ab"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2486), null, null }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1db2efa1-303b-41da-9c63-6753506ddc49"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 13, 55, 9, 426, DateTimeKind.Utc).AddTicks(4075));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6bf8ddbb-4aac-40fb-8972-c91718401175"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 13, 55, 9, 426, DateTimeKind.Utc).AddTicks(4073));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("86a13262-7fca-4b0f-b3f0-d28c48133034"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 13, 55, 9, 426, DateTimeKind.Utc).AddTicks(4077));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "CreatedBy", "CreatedDate", "Email", "FullName", "Gender", "PasswordHash", "Phone", "RoleId", "RoleId1", "Status", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("99d3b346-bd2a-4862-98c2-b7b07fef4ba8"), "Thạch Thất, Hòa Lạc", "", null, new DateTime(2025, 3, 21, 13, 55, 9, 644, DateTimeKind.Utc).AddTicks(2701), "owner@gmail.com", "John Doe", 1, "$2a$10$WFL7Y3UvKS0Gks/pRHWB7.Aj.D8IhPUpv.S0Wbl1Fy60Vzdc1nQfq", "0987654321", new Guid("86a13262-7fca-4b0f-b3f0-d28c48133034"), null, 1, null, null },
                    { new Guid("9fed27f3-895c-4f42-8baf-7497f24bbbfe"), "Thạch Thất, Hòa Lạc", "", null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(1830), "customer@gmail.com", "Jane Smith", 2, "$2a$10$QZwD.wt54qB53EgSOI0DnucKntwRMWJf4rY9kHJh.cZ/y88xrzCbO", "0123987654", new Guid("1db2efa1-303b-41da-9c63-6753506ddc49"), null, 1, null, null },
                    { new Guid("b0a84411-fa51-454f-ad67-c70077f3bc31"), "System Address", "", null, new DateTime(2025, 3, 21, 13, 55, 9, 533, DateTimeKind.Utc).AddTicks(5405), "admin@gmail.com", "System Admin", 1, "$2a$10$eElydbMFNVXA8pYeif4j.Om9GTqiU7Ys0aM3eyMeeD8Wdy0s2beH2", "0123456789", new Guid("6bf8ddbb-4aac-40fb-8972-c91718401175"), null, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "FacilityTimeSlots",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EndTime", "FacilityId", "IsWeekend", "StartTime", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2559), new TimeSpan(0, 12, 30, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), false, new TimeSpan(0, 11, 0, 0, 0), null, null },
                    { new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2556), new TimeSpan(0, 9, 30, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), false, new TimeSpan(0, 8, 0, 0, 0), null, null },
                    { new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2563), new TimeSpan(0, 17, 0, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), false, new TimeSpan(0, 15, 30, 0, 0), null, null },
                    { new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2558), new TimeSpan(0, 11, 0, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), false, new TimeSpan(0, 9, 30, 0, 0), null, null },
                    { new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2569), new TimeSpan(0, 11, 0, 0, 0), new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), false, new TimeSpan(0, 9, 30, 0, 0), null, null },
                    { new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2564), new TimeSpan(0, 9, 30, 0, 0), new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), false, new TimeSpan(0, 8, 0, 0, 0), null, null },
                    { new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2561), new TimeSpan(0, 15, 30, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), false, new TimeSpan(0, 14, 0, 0, 0), null, null },
                    { new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"), null, new DateTime(2025, 3, 21, 13, 55, 9, 753, DateTimeKind.Utc).AddTicks(2571), new TimeSpan(0, 12, 30, 0, 0), new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), false, new TimeSpan(0, 11, 0, 0, 0), null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("19a0d469-93c9-4158-be0b-daf94107cb46"));

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: new Guid("957c4719-4bca-4ce0-9b55-ced6573f76d2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99d3b346-bd2a-4862-98c2-b7b07fef4ba8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9fed27f3-895c-4f42-8baf-7497f24bbbfe"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b0a84411-fa51-454f-ad67-c70077f3bc31"));

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"));

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 9, 31, 23, 720, DateTimeKind.Utc).AddTicks(8509));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 9, 31, 23, 720, DateTimeKind.Utc).AddTicks(8510));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 9, 31, 23, 720, DateTimeKind.Utc).AddTicks(8512));

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "Capacity", "CategoryId", "CreatedBy", "CreatedDate", "Description", "Images", "Location", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("241dcce9-51fb-413c-a653-5892d5cac69c"), "4", new Guid("b1234567-1234-1234-1234-1234567890bc"), null, new DateTime(2025, 3, 21, 9, 31, 23, 720, DateTimeKind.Utc).AddTicks(8612), "Sân cầu lông đơn/đôi", "image2.jpg", "Thạch Thất, Hòa Lạc", "Badminton Court 1", null, null },
                    { new Guid("9668b4d4-936f-4a74-866b-915947fb0b33"), "10", new Guid("a1234567-1234-1234-1234-1234567890ab"), null, new DateTime(2025, 3, 21, 9, 31, 23, 720, DateTimeKind.Utc).AddTicks(8610), "Sân bóng đá 5 người", "image1.jpg", "Thạch Thất, Hòa Lạc", "Football Field 5-a-side", null, null }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "BasePrice", "CategoryId", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1a58a24b-79ee-4a6d-ad48-0ae82d889b17"), 400000m, new Guid("a1234567-1234-1234-1234-1234567890ab"), null, new DateTime(2025, 3, 21, 9, 31, 23, 720, DateTimeKind.Utc).AddTicks(8664), null, null },
                    { new Guid("75e5fa69-f123-4c8b-9c59-97e1830a01fb"), 200000m, new Guid("b1234567-1234-1234-1234-1234567890bc"), null, new DateTime(2025, 3, 21, 9, 31, 23, 720, DateTimeKind.Utc).AddTicks(8665), null, null }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1db2efa1-303b-41da-9c63-6753506ddc49"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 9, 31, 23, 374, DateTimeKind.Utc).AddTicks(1985));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6bf8ddbb-4aac-40fb-8972-c91718401175"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 9, 31, 23, 374, DateTimeKind.Utc).AddTicks(1983));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("86a13262-7fca-4b0f-b3f0-d28c48133034"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 21, 9, 31, 23, 374, DateTimeKind.Utc).AddTicks(1986));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "CreatedBy", "CreatedDate", "Email", "FullName", "Gender", "PasswordHash", "Phone", "RoleId", "RoleId1", "Status", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("895d096a-4778-4838-87df-980b2f5f5844"), "Thạch Thất, Hòa Lạc", "", null, new DateTime(2025, 3, 21, 9, 31, 23, 577, DateTimeKind.Utc).AddTicks(78), "owner@gmail.com", "John Doe", 1, "$2a$10$ahBhc6xycG4fOqVx7IgfpOsL7CpbyE6Mq7g5IUmkQcgHJdPrgIk2O", "0987654321", new Guid("86a13262-7fca-4b0f-b3f0-d28c48133034"), null, 1, null, null },
                    { new Guid("a37800b7-18a0-4a42-887a-f4868a4dc244"), "System Address", "", null, new DateTime(2025, 3, 21, 9, 31, 23, 473, DateTimeKind.Utc).AddTicks(8426), "admin@gmail.com", "System Admin", 1, "$2a$10$oJ8463fMIZ5wZ5OlW/dfSOY6v3FXWx7JAD1fVohwKe1fhI2fH0a8q", "0123456789", new Guid("6bf8ddbb-4aac-40fb-8972-c91718401175"), null, 1, null, null },
                    { new Guid("fab7f104-71e3-481b-bf06-e4df2c5ba999"), "Thạch Thất, Hòa Lạc", "", null, new DateTime(2025, 3, 21, 9, 31, 23, 720, DateTimeKind.Utc).AddTicks(8011), "customer@gmail.com", "Jane Smith", 2, "$2a$10$kam1lwZaMCNr9rkF7HReMetm7f5Uqg3aLuj6nv1J73MDMcczH6pvS", "0123987654", new Guid("1db2efa1-303b-41da-9c63-6753506ddc49"), null, 1, null, null }
                });
        }
    }
}
