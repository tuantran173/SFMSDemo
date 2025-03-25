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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Images = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
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
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

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
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FacilityTimeSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsWeekend = table.Column<bool>(type: "bit", nullable: false),
                    FacilityId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityTimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityTimeSlots_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacilityTimeSlots_Facilities_FacilityId1",
                        column: x => x.FacilityId1,
                        principalTable: "Facilities",
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
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Expiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacilityPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityTimeSlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Coefficient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityPrices_FacilityTimeSlots_FacilityTimeSlotId",
                        column: x => x.FacilityTimeSlotId,
                        principalTable: "FacilityTimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("a1234567-1234-1234-1234-1234567890ab"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6422), "Sân bóng đá 5-a-side, 7-a-side, 11-a-side", "Sân bóng", null, null },
                    { new Guid("b1234567-1234-1234-1234-1234567890bc"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6424), "Sân cầu lông đơn và đôi", "Sân cầu lông", null, null },
                    { new Guid("c1234567-1234-1234-1234-1234567890cd"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6425), "Sân Pickleball chuẩn quốc tế", "Sân Pickleball", null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Name", "RoleCode", "Status", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1db2efa1-303b-41da-9c63-6753506ddc49"), null, new DateTime(2025, 3, 24, 8, 30, 23, 705, DateTimeKind.Utc).AddTicks(2274), "Customer", "CUS", 1, null, null },
                    { new Guid("6bf8ddbb-4aac-40fb-8972-c91718401175"), null, new DateTime(2025, 3, 24, 8, 30, 23, 705, DateTimeKind.Utc).AddTicks(2272), "Admin", "AD", 1, null, null },
                    { new Guid("86a13262-7fca-4b0f-b3f0-d28c48133034"), null, new DateTime(2025, 3, 24, 8, 30, 23, 705, DateTimeKind.Utc).AddTicks(2275), "Owner", "OWN", 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "Capacity", "CategoryId", "CreatedBy", "CreatedDate", "Description", "Images", "Location", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), "4", new Guid("b1234567-1234-1234-1234-1234567890bc"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6485), "Sân cầu lông đơn/đôi", "image2.jpg", "Thạch Thất, Hòa Lạc", "Badminton Court 1", null, null },
                    { new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), "10", new Guid("a1234567-1234-1234-1234-1234567890ab"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6482), "Sân bóng đá 5 người", "image1.jpg", "Thạch Thất, Hòa Lạc", "Football Field 5-a-side", null, null }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "BasePrice", "CategoryId", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("14f15154-3616-43f5-8097-14c87b18e67f"), 200000m, new Guid("b1234567-1234-1234-1234-1234567890bc"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6579), null, null },
                    { new Guid("9e7b0e37-d746-4b36-8d5f-4f6f001dfac9"), 400000m, new Guid("a1234567-1234-1234-1234-1234567890ab"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6577), null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "CreatedBy", "CreatedDate", "Email", "FullName", "Gender", "PasswordHash", "Phone", "RoleId", "RoleId1", "Status", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("a1234368-1234-1234-1234-1234567830ab"), "Thạch Thất, Hòa Lạc", "", null, new DateTime(2025, 3, 24, 8, 30, 23, 955, DateTimeKind.Utc).AddTicks(17), "owner@gmail.com", "John Doe", 1, "$2a$10$LhntqUawxKuD994Iz1kMAuTakPqyUwiSRGULr8o.Bu2swaufbA2XC", "0987654321", new Guid("86a13262-7fca-4b0f-b3f0-d28c48133034"), null, 1, null, null },
                    { new Guid("a1234568-1234-1234-1234-1234567810ab"), "System Address", "", null, new DateTime(2025, 3, 24, 8, 30, 23, 850, DateTimeKind.Utc).AddTicks(8496), "admin@gmail.com", "System Admin", 1, "$2a$10$ohhPKLZ7Un8xugJ8v2jkkeoHsG5Lnl7NJO4WXkjRhlatELNoIIQxa", "0123456789", new Guid("6bf8ddbb-4aac-40fb-8972-c91718401175"), null, 1, null, null },
                    { new Guid("a1934568-1234-1234-1234-1234562810ab"), "Thạch Thất, Hòa Lạc", "", null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(5942), "customer@gmail.com", "Jane Smith", 2, "$2a$10$8Sc87GReOOF62nTYu84Sluzu6y4PiPy5NP/svoxQlGpvsRIwIZLsm", "0123987654", new Guid("1db2efa1-303b-41da-9c63-6753506ddc49"), null, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "FacilityTimeSlots",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EndTime", "FacilityId", "FacilityId1", "IsWeekend", "StartTime", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1a2c6a93-97cd-4493-a1fc-9b5819ac6e17"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6647), new TimeSpan(0, 12, 30, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), null, false, new TimeSpan(0, 11, 0, 0, 0), null, null },
                    { new Guid("1b05b57c-6d02-4c06-b0b5-a96139825346"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6644), new TimeSpan(0, 9, 30, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), null, false, new TimeSpan(0, 8, 0, 0, 0), null, null },
                    { new Guid("1b7ea0d1-c743-47d7-b3f1-02860dbd9806"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6649), new TimeSpan(0, 17, 0, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), null, false, new TimeSpan(0, 15, 30, 0, 0), null, null },
                    { new Guid("907b662c-5a2c-4a90-b96b-81b603b27e57"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6645), new TimeSpan(0, 11, 0, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), null, false, new TimeSpan(0, 9, 30, 0, 0), null, null },
                    { new Guid("b03366d1-b1cc-4c0e-8e61-6fff1651755d"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6657), new TimeSpan(0, 11, 0, 0, 0), new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), null, false, new TimeSpan(0, 9, 30, 0, 0), null, null },
                    { new Guid("bb9299e1-518a-4730-9797-6ec37c5dd03f"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6651), new TimeSpan(0, 9, 30, 0, 0), new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), null, false, new TimeSpan(0, 8, 0, 0, 0), null, null },
                    { new Guid("d75d092a-7da6-4cc3-88c9-69ac5c82652c"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6648), new TimeSpan(0, 15, 30, 0, 0), new Guid("f34c777a-fa4b-4ed1-bc22-29570a01d7d9"), null, false, new TimeSpan(0, 14, 0, 0, 0), null, null },
                    { new Guid("ffa61f3b-58a0-4881-ae97-61332f81fc4f"), null, new DateTime(2025, 3, 24, 8, 30, 24, 59, DateTimeKind.Utc).AddTicks(6658), new TimeSpan(0, 12, 30, 0, 0), new Guid("9eefd023-7cc3-428f-b96d-3e0430394391"), null, false, new TimeSpan(0, 11, 0, 0, 0), null, null }
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
                name: "IX_Facilities_CategoryId",
                table: "Facilities",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityPrices_FacilityTimeSlotId",
                table: "FacilityPrices",
                column: "FacilityTimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityTimeSlots_FacilityId",
                table: "FacilityTimeSlots",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityTimeSlots_FacilityId1",
                table: "FacilityTimeSlots",
                column: "FacilityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CategoryId",
                table: "Prices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId1",
                table: "Users",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "FacilityPrices");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "FacilityTimeSlots");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
