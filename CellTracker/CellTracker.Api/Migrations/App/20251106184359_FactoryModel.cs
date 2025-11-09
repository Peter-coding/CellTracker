using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CellTracker.Api.App
{
    /// <inheritdoc />
    public partial class FactoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "cell_tracker",
                table: "OperatorTasks",
                keyColumn: "Id",
                keyValue: new Guid("35031d70-8287-4bfe-bd63-05a816f44444"));

            migrationBuilder.DeleteData(
                schema: "cell_tracker",
                table: "OperatorTasks",
                keyColumn: "Id",
                keyValue: new Guid("35031d70-8287-4bfe-bd63-05a816f44666"));

            migrationBuilder.CreateTable(
                name: "Factories",
                schema: "cell_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteUser",
                schema: "cell_tracker",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    LastName = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    LoginCode = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionLines",
                schema: "cell_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    OrdinalNumber = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FactoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionLines_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalSchema: "cell_tracker",
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "cell_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Token = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_SiteUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "cell_tracker",
                        principalTable: "SiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cells",
                schema: "cell_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    OrdinalNumber = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductionLineId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cells_ProductionLines_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalSchema: "cell_tracker",
                        principalTable: "ProductionLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkStations",
                schema: "cell_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MqttDeviceId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    OrdinalNumber = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CellId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkStations_Cells_CellId",
                        column: x => x.CellId,
                        principalSchema: "cell_tracker",
                        principalTable: "Cells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "Factories",
                columns: new[] { "Id", "Address", "City", "Country", "CreatedAt", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Industrial Park 12", "Budapest", "Hungary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@mainfactory.hu", "Main Factory", "+36 1 555 1234" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Technológia u. 4.", "Győr", "Hungary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@backupplant.hu", "Backup Plant", "+36 96 444 777" }
                });

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "SiteUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "LoginCode", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "35031d70-8287-4bfe-bd63-05a816f44885", 0, "494850a4-9f8c-46b2-97d5-f2b406dcc6f3", "test.user1@example.com", true, "Test1", "User1", false, null, null, "TEST.USER1@EXAMPLE.COM", "TESTUSER1", "0000000", null, false, "35031d70-8287-4bfe-bd63-05a816f44880", false, "testuser1" },
                    { "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5", 0, "016a5a34-7e14-4d0f-b1f7-6041fb9d147a", "test.user@example.com", true, "Test", "User", false, null, null, "TEST.USER@EXAMPLE.COM", "TESTUSER", "1111111", null, false, "ee6739f0-97e2-496c-8ffe-74a39ae7a8e4", false, "testuser" }
                });

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "ProductionLines",
                columns: new[] { "Id", "CreatedAt", "Description", "FactoryId", "Name", "OrdinalNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automotive ECU assembly line", new Guid("11111111-1111-1111-1111-111111111111"), "Assembly Line A", (short)1, 0 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Battery module production", new Guid("11111111-1111-1111-1111-111111111111"), "Assembly Line B", (short)2, 2 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final packaging and quality control", new Guid("22222222-2222-2222-2222-222222222222"), "Packaging Line 1", (short)1, 1 }
                });

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "Cells",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "OrdinalNumber", "ProductionLineId" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robotized soldering unit", "Soldering Cell", 1, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Optical quality inspection station", "Inspection Cell", 2, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Box sealing and labeling unit", "Packaging Cell", 1, new Guid("55555555-5555-5555-5555-555555555555") }
                });

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "WorkStations",
                columns: new[] { "Id", "CellId", "CreatedAt", "Description", "MqttDeviceId", "Name", "OrdinalNumber" },
                values: new object[,]
                {
                    { new Guid("99999999-9999-9999-9999-999999999999"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manual soldering station", "A1", "WS-SLD-01", (short)1 },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automated soldering robot arm", "A2", "WS-SLD-02", (short)2 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Visual inspection camera station", "A3", "WS-INS-01", (short)1 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Box assembly station", "A4", "WS-PKG-01", (short)1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cells_ProductionLineId",
                schema: "cell_tracker",
                table: "Cells",
                column: "ProductionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLines_FactoryId",
                schema: "cell_tracker",
                table: "ProductionLines",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Token",
                schema: "cell_tracker",
                table: "RefreshToken",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "cell_tracker",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkStations_CellId",
                schema: "cell_tracker",
                table: "WorkStations",
                column: "CellId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "cell_tracker");

            migrationBuilder.DropTable(
                name: "WorkStations",
                schema: "cell_tracker");

            migrationBuilder.DropTable(
                name: "SiteUser",
                schema: "cell_tracker");

            migrationBuilder.DropTable(
                name: "Cells",
                schema: "cell_tracker");

            migrationBuilder.DropTable(
                name: "ProductionLines",
                schema: "cell_tracker");

            migrationBuilder.DropTable(
                name: "Factories",
                schema: "cell_tracker");

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "OperatorTasks",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "QuantityGoal" },
                values: new object[,]
                {
                    { new Guid("35031d70-8287-4bfe-bd63-05a816f44444"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sample operator task", "Test Task", 10 },
                    { new Guid("35031d70-8287-4bfe-bd63-05a816f44666"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sample operator task2", "Test Task2", 15 }
                });
        }
    }
}
