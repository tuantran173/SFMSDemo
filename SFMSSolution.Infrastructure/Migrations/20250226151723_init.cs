using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SFMSSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Capacity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Images = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ResetPasswordTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "Capacity", "CreatedDate", "Description", "Images", "Location", "Name", "Price", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("637e671f-6ea1-4e4e-aa7c-5fdd1db1b10f"), "4", new DateTime(2025, 2, 26, 15, 17, 20, 765, DateTimeKind.Utc).AddTicks(8077), "Sân cầu lông trong nhà, điều hòa, thích hợp cho giải đấu.", "/uploads/facilities/sanh_tennis_b.jpg", "Thôn 3, Thạch Hòa, Thạch Thất, Hà Nội", "Sân cầu lông", 300m, "Under Maintenance", null },
                    { new Guid("ada9093b-0b25-4afd-9b2f-efc760aed770"), "22", new DateTime(2025, 2, 26, 15, 17, 20, 765, DateTimeKind.Utc).AddTicks(8074), "Sân bóng cỏ tự nhiên với trang thiết bị hiện đại.", "/uploads/facilities/sanh_bong_a.jpg", "Thôn 3, Thạch Hòa, Thạch Thất, Hà Nội", "Sân bóng", 500m, "Available", null },
                    { new Guid("cae13754-8328-4ffa-a092-1822ff6c3c70"), "30", new DateTime(2025, 2, 26, 15, 17, 20, 765, DateTimeKind.Utc).AddTicks(8079), "Sân Pickleball hiện đại với trang thiết bị hiện đại.", "/uploads/facilities/phong_gym_c.jpg", "Thôn 3, Thạch Hòa, Thạch Thất, Hà Nội", "Sân Pickleball", 100m, "Closed", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "Name", "RoleCode", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("06324a24-cb26-4452-a3b8-731e1b980e5f"), new DateTime(2025, 2, 26, 15, 17, 20, 430, DateTimeKind.Utc).AddTicks(4684), "Admin", "AD", 1, null },
                    { new Guid("ca39c26a-406e-424b-bab0-389b6efe38ed"), new DateTime(2025, 2, 26, 15, 17, 20, 430, DateTimeKind.Utc).AddTicks(4701), "Customer", "CUS", 1, null },
                    { new Guid("dc8b1c2b-91b6-4160-b93e-d5a3b24d8f5d"), new DateTime(2025, 2, 26, 15, 17, 20, 430, DateTimeKind.Utc).AddTicks(4699), "Facility Owner", "FO", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Deleted", "Email", "FullName", "Gender", "PasswordHash", "Phone", "RefreshToken", "RefreshTokenExpiry", "ResetPasswordToken", "ResetPasswordTokenExpiry", "RoleId", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("17123670-e7fc-4656-932d-1d2525e9c1c0"), new DateTime(2025, 2, 26, 15, 17, 20, 652, DateTimeKind.Utc).AddTicks(4499), false, "owner@gmail.com", "Facility Owner", 2, "$2a$10$k0V3eqD4C2Bd8bMWz3Ujt.bKCm3c4.0.YJVU.rqIv1qHiTt1y40Ke", "0987654321", "", null, "", null, null, 1, null },
                    { new Guid("a377cd0a-560f-4182-a046-903fa0b04434"), new DateTime(2025, 2, 26, 15, 17, 20, 541, DateTimeKind.Utc).AddTicks(1912), false, "admin@gmail.com", "Admin", 1, "$2a$10$KgCro0Swuxx8Qlu5iIkET..OSuyVZ4D0LuE3MDi0dsDfHDaotgYPi", "0974209212", "", null, "", null, null, 1, null },
                    { new Guid("dff0783d-10b7-4aa9-9fdc-364591a7c45b"), new DateTime(2025, 2, 26, 15, 17, 20, 765, DateTimeKind.Utc).AddTicks(7439), false, "customer@gmail.com", "Customer", 1, "$2a$10$dZ1POxZrYHlIltD4X0g2/OQD4qX.G4/6yduRDy0LhNIqJWcT9zx0W", "0112233445", "", null, "", null, null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CreatedDate", "EndTime", "FacilityId", "StartTime", "Status", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("6b0cafdc-c84c-4474-be38-14be5e5cb002"), new DateTime(2025, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 26, 15, 17, 20, 765, DateTimeKind.Utc).AddTicks(8140), new DateTime(2025, 2, 27, 11, 0, 0, 0, DateTimeKind.Utc), new Guid("ada9093b-0b25-4afd-9b2f-efc760aed770"), new DateTime(2025, 2, 27, 9, 0, 0, 0, DateTimeKind.Utc), "Pending", null, new Guid("a377cd0a-560f-4182-a046-903fa0b04434") },
                    { new Guid("ef5dac8e-39b7-4388-83b8-604a47729b46"), new DateTime(2025, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 26, 15, 17, 20, 765, DateTimeKind.Utc).AddTicks(8147), new DateTime(2025, 2, 27, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("637e671f-6ea1-4e4e-aa7c-5fdd1db1b10f"), new DateTime(2025, 2, 27, 14, 0, 0, 0, DateTimeKind.Utc), "Confirmed", null, new Guid("dff0783d-10b7-4aa9-9fdc-364591a7c45b") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("dc8b1c2b-91b6-4160-b93e-d5a3b24d8f5d"), new Guid("17123670-e7fc-4656-932d-1d2525e9c1c0") },
                    { new Guid("06324a24-cb26-4452-a3b8-731e1b980e5f"), new Guid("a377cd0a-560f-4182-a046-903fa0b04434") },
                    { new Guid("ca39c26a-406e-424b-bab0-389b6efe38ed"), new Guid("dff0783d-10b7-4aa9-9fdc-364591a7c45b") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FacilityId",
                table: "Bookings",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
