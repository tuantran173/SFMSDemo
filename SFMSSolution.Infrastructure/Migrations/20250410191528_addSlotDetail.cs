using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSlotDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlotDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    SlotId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlotDetails_FacilityTimeSlots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "FacilityTimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2524));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2527));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2635));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2638));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2637));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2564));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2562));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2598));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2595));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2601));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2597));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2604));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2602));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 10, 19, 15, 25, 136, DateTimeKind.Utc).AddTicks(2605));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "56de0085-829c-4cd2-8981-278498dc1a7f");

            migrationBuilder.CreateIndex(
                name: "IX_SlotDetails_SlotId_Date_StartTime_EndTime",
                table: "SlotDetails",
                columns: new[] { "SlotId", "Date", "StartTime", "EndTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlotDetails");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-1234-1234-1234-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6809));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-1234-1234-1234-1234567890bc"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6811));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-1234-1234-1234-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6812));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("abcd1234-abcd-1234-abcd-1234567890ab"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6919));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dcba4321-dcba-4321-dcba-0987654321ef"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6922));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("efef5678-efef-5678-efef-1234567890cd"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6920));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6849));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6846));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6882));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6879));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6885));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6881));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6888));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6887));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6884));

            migrationBuilder.UpdateData(
                table: "FacilityTimeSlots",
                keyColumn: "Id",
                keyValue: new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"),
                column: "CreatedDate",
                value: new DateTime(2025, 4, 9, 8, 20, 38, 904, DateTimeKind.Utc).AddTicks(6890));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ConcurrencyStamp",
                value: "997e8298-d194-4519-90c2-d8b94e0a9ea9");
        }
    }
}
