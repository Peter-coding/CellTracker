using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CellTracker.Api.Migrations.App
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cell_tracker");

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
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatorTasks",
                schema: "cell_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    QuantityGoal = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorTasks", x => x.Id);
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
                    OrdinalNumber = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FactoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
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
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
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
                name: "Simulations",
                schema: "cell_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Shift = table.Column<int>(type: "integer", nullable: false),
                    NumberOfProductsMade = table.Column<int>(type: "integer", nullable: false),
                    MinutesOfSimulation = table.Column<int>(type: "integer", nullable: false),
                    ProductionLineId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Simulations_ProductionLines_ProductionLineId",
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
                    OrdinalNumber = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CellId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
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
                columns: new[] { "Id", "Address", "City", "Country", "CreatedAt", "Email", "IsDeleted", "ModifiedAt", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Industrial Park 12", "Budapest", "Hungary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@mainfactory.hu", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Main Factory", "+36 1 555 1234" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Technológia u. 4.", "Győr", "Hungary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@backupplant.hu", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Backup Plant", "+36 96 444 777" },
                    { new Guid("33333333-aaaa-bbbb-cccc-111111111111"), "Werkstrasse 8", "Stuttgart", "Germany", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@electronicsplant.de", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electronics Plant", "+49 711 123 456" },
                    { new Guid("44444444-aaaa-bbbb-cccc-222222222222"), "Technicka 22", "Brno", "Czech Republic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@automationhub.cz", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automation Hub", "+420 555 987 654" }
                });

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "SiteUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "LoginCode", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "35031d70-8287-4bfe-bd63-05a816f44885", 0, "6916c252-7a94-49a5-aa43-7ec194af2a1f", "test.user1@example.com", true, "Test1", "User1", false, null, null, "TEST.USER1@EXAMPLE.COM", "TESTUSER1", "0000000", null, false, "35031d70-8287-4bfe-bd63-05a816f44880", false, "testuser1" },
                    { "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5", 0, "b1dad2a8-50e9-4261-9e6c-860f22920143", "test.user@example.com", true, "Test", "User", false, null, null, "TEST.USER@EXAMPLE.COM", "TESTUSER", "1111111", null, false, "ee6739f0-97e2-496c-8ffe-74a39ae7a8e4", false, "testuser" }
                });

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "ProductionLines",
                columns: new[] { "Id", "CreatedAt", "Description", "FactoryId", "IsDeleted", "ModifiedAt", "Name", "OrdinalNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automotive ECU assembly line", new Guid("11111111-1111-1111-1111-111111111111"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assembly Line A", 1, 0 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Battery module production", new Guid("11111111-1111-1111-1111-111111111111"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assembly Line B", 2, 2 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final packaging and quality control", new Guid("22222222-2222-2222-2222-222222222222"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Packaging Line 1", 3, 1 },
                    { new Guid("66666666-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Motherboard soldering and assembly", new Guid("33333333-aaaa-bbbb-cccc-111111111111"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PCB Line 1", 4, 0 },
                    { new Guid("77777777-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-density PCB assembly", new Guid("33333333-aaaa-bbbb-cccc-111111111111"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PCB Line 2", 5, 2 },
                    { new Guid("88888888-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electronic unit final assembly", new Guid("33333333-aaaa-bbbb-cccc-111111111111"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final Assembly Line", 6, 0 },
                    { new Guid("99999999-aaaa-bbbb-cccc-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Industrial robot assembly", new Guid("44444444-aaaa-bbbb-cccc-222222222222"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automation Line 1", 7, 1 },
                    { new Guid("aaaaaaaa-aaaa-bbbb-cccc-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robot calibration and testing line", new Guid("44444444-aaaa-bbbb-cccc-222222222222"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automation Line 2", 8, 0 },
                    { new Guid("bbbbbbbb-aaaa-bbbb-cccc-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AGV control unit production", new Guid("44444444-aaaa-bbbb-cccc-222222222222"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automation Line 3", 9, 2 }
                });

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "Cells",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "ModifiedAt", "Name", "OrdinalNumber", "ProductionLineId" },
                values: new object[,]
                {
                    { new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Preparation", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solder Prep", 1, new Guid("66666666-aaaa-bbbb-cccc-111111111111") },
                    { new Guid("11111111-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wave solder station", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soldering", 2, new Guid("66666666-aaaa-bbbb-cccc-111111111111") },
                    { new Guid("11111111-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flux removal", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cleaning", 3, new Guid("66666666-aaaa-bbbb-cccc-111111111111") },
                    { new Guid("22222222-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chip placement", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pick&Place", 1, new Guid("77777777-aaaa-bbbb-cccc-111111111111") },
                    { new Guid("22222222-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Overheating tunnel", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reflow", 2, new Guid("77777777-aaaa-bbbb-cccc-111111111111") },
                    { new Guid("22222222-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Optical inspection", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AOI", 3, new Guid("77777777-aaaa-bbbb-cccc-111111111111") },
                    { new Guid("33333333-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chassis assembly", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Housing Assembly", 1, new Guid("88888888-aaaa-bbbb-cccc-111111111111") },
                    { new Guid("33333333-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Firmware flashing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Firmware Load", 2, new Guid("88888888-aaaa-bbbb-cccc-111111111111") },
                    { new Guid("33333333-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final quality control", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final QC", 3, new Guid("88888888-aaaa-bbbb-cccc-111111111111") },
                    { new Guid("44444444-aaaa-bbbb-cccc-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arm modules", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robot Arm Assembly", 1, new Guid("99999999-aaaa-bbbb-cccc-222222222222") },
                    { new Guid("44444444-bbbb-cccc-dddd-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Servo motors", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Motor Assembly", 2, new Guid("99999999-aaaa-bbbb-cccc-222222222222") },
                    { new Guid("44444444-cccc-dddd-eeee-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Safety certification", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Safety Check", 3, new Guid("99999999-aaaa-bbbb-cccc-222222222222") },
                    { new Guid("55555555-aaaa-bbbb-cccc-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Initial calibration", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Calibration Base", 1, new Guid("aaaaaaaa-aaaa-bbbb-cccc-222222222222") },
                    { new Guid("55555555-bbbb-cccc-dddd-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Camera alignment", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vision Alignment", 2, new Guid("aaaaaaaa-aaaa-bbbb-cccc-222222222222") },
                    { new Guid("55555555-cccc-dddd-eeee-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mechanical testing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stress Test", 3, new Guid("aaaaaaaa-aaaa-bbbb-cccc-222222222222") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robotized soldering unit", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soldering Cell", 1, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("66666666-aaaa-bbbb-cccc-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PCB assembly", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AGV Control Board", 1, new Guid("bbbbbbbb-aaaa-bbbb-cccc-222222222222") },
                    { new Guid("66666666-bbbb-cccc-dddd-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sensors installation", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lidar Install", 2, new Guid("bbbbbbbb-aaaa-bbbb-cccc-222222222222") },
                    { new Guid("66666666-cccc-dddd-eeee-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AGV nav calibration", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Navigation Test", 3, new Guid("bbbbbbbb-aaaa-bbbb-cccc-222222222222") },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Optical quality inspection station", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inspection Cell", 2, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("88888888-8888-8888-8888-888888888881"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Box sealing and labeling unit2", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Packaging Cell2", 4, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("88888888-8888-8888-8888-888888888882"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Box sealing and labeling unit3", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Packaging Cell3", 5, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("88888888-8888-8888-8888-888888888883"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Box sealing and labeling unit4", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Packaging Cell4", 6, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Box sealing and labeling unit1", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Packaging Cell1", 3, new Guid("33333333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "Simulations",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "MinutesOfSimulation", "ModifiedAt", "NumberOfProductsMade", "ProductionLineId", "Shift" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 145, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 198, new Guid("33333333-3333-3333-3333-333333333333"), 1 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 170, new Guid("44444444-4444-4444-4444-444444444444"), 2 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 220, new Guid("44444444-4444-4444-4444-444444444444"), 0 },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 260, new Guid("55555555-5555-5555-5555-555555555555"), 1 }
                });

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "WorkStations",
                columns: new[] { "Id", "CellId", "CreatedAt", "Description", "IsDeleted", "ModifiedAt", "MqttDeviceId", "Name", "OrdinalNumber" },
                values: new object[,]
                {
                    { new Guid("10000000-aaaa-bbbb-cccc-111111111111"), new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solder prep station #1", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B1", "WS-SP-01", 1 },
                    { new Guid("10000000-aaaa-bbbb-cccc-111111111112"), new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solder prep station #2", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B2", "WS-SP-02", 2 },
                    { new Guid("10000000-aaaa-bbbb-cccc-111111111113"), new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solder prep station #3", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B3", "WS-SP-03", 3 },
                    { new Guid("10000000-aaaa-bbbb-cccc-111111111114"), new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solder prep station #4", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B4", "WS-SP-04", 4 },
                    { new Guid("10000000-aaaa-bbbb-cccc-111111111115"), new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solder prep station #5", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B5", "WS-SP-05", 5 },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Precision soldering microstation", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A3", "WS-SLD-03", 3 },
                    { new Guid("11111111-1111-1111-1111-111111111112"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-temperature soldering unit", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A4", "WS-SLD-04", 4 },
                    { new Guid("11111111-1111-1111-1111-111111111113"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flux application station", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A5", "WS-SLD-05", 5 },
                    { new Guid("11111111-1111-1111-1111-111111111114"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reflow soldering preparation", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A6", "WS-SLD-06", 6 },
                    { new Guid("11111111-1111-1111-1111-111111111115"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post-soldering cooling point", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A7", "WS-SLD-07", 7 },
                    { new Guid("20000000-aaaa-bbbb-cccc-111111111111"), new Guid("11111111-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wave solder #1", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B6", "WS-SLD-01", 1 },
                    { new Guid("20000000-aaaa-bbbb-cccc-111111111112"), new Guid("11111111-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wave solder #2", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B7", "WS-SLD-02", 2 },
                    { new Guid("20000000-aaaa-bbbb-cccc-111111111113"), new Guid("11111111-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wave solder #3", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B8", "WS-SLD-03", 3 },
                    { new Guid("20000000-aaaa-bbbb-cccc-111111111114"), new Guid("11111111-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wave solder #4", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B9", "WS-SLD-04", 4 },
                    { new Guid("20000000-aaaa-bbbb-cccc-111111111115"), new Guid("11111111-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wave solder #5", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B10", "WS-SLD-05", 5 },
                    { new Guid("22222222-2222-2222-2222-222222222221"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AOI optical scan unit", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A9", "WS-INS-02", 2 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-resolution defect camera", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A13", "WS-INS-03", 6 },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automated dimension check sensor", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A10", "WS-INS-04", 3 },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barcode and label verification", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A11", "WS-INS-05", 4 },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quality classification station", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A12", "WS-INS-06", 5 },
                    { new Guid("30000000-aaaa-bbbb-cccc-111111111111"), new Guid("11111111-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cleaning station #1", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B11", "WS-CLN-01", 1 },
                    { new Guid("30000000-aaaa-bbbb-cccc-111111111112"), new Guid("11111111-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cleaning station #2", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B12", "WS-CLN-02", 2 },
                    { new Guid("30000000-aaaa-bbbb-cccc-111111111113"), new Guid("11111111-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cleaning station #3", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B13", "WS-CLN-03", 3 },
                    { new Guid("30000000-aaaa-bbbb-cccc-111111111114"), new Guid("11111111-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cleaning station #4", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B14", "WS-CLN-04", 4 },
                    { new Guid("30000000-aaaa-bbbb-cccc-111111111115"), new Guid("11111111-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cleaning station #5", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B15", "WS-CLN-05", 5 },
                    { new Guid("33333333-3333-3333-3333-333333333331"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automated carton forming", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A19", "WS-PKG0-01", 6 },
                    { new Guid("33333333-3333-3333-3333-333333333332"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filling alignment station", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A15", "WS-PKG0-02", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Protective foam insertion unit", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A16", "WS-PKG0-03", 3 },
                    { new Guid("33333333-3333-3333-3333-333333333334"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automated box sealing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A17", "WS-PKG0-04", 4 },
                    { new Guid("33333333-3333-3333-3333-333333333335"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Package weight verification", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A18", "WS-PKG0-05", 5 },
                    { new Guid("40000000-aaaa-bbbb-cccc-111111111111"), new Guid("22222222-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pick&Place #1", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "D1", "WS-PP-01", 1 },
                    { new Guid("40000000-aaaa-bbbb-cccc-111111111112"), new Guid("22222222-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pick&Place #2", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "D2", "WS-PP-02", 2 },
                    { new Guid("40000000-aaaa-bbbb-cccc-111111111113"), new Guid("22222222-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pick&Place #3", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "D3", "WS-PP-03", 3 },
                    { new Guid("40000000-aaaa-bbbb-cccc-111111111114"), new Guid("22222222-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pick&Place #4", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "D4", "WS-PP-04", 4 },
                    { new Guid("40000000-aaaa-bbbb-cccc-111111111115"), new Guid("22222222-aaaa-bbbb-cccc-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pick&Place #5", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "D5", "WS-PP-05", 5 },
                    { new Guid("44444444-4444-4444-4444-444444444441"), new Guid("88888888-8888-8888-8888-888888888881"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automated box printing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A20", "WS-PKG1-01", 1 },
                    { new Guid("44444444-4444-4444-4444-444444444442"), new Guid("88888888-8888-8888-8888-888888888881"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RFID label application", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A21", "WS-PKG1-02", 2 },
                    { new Guid("44444444-4444-4444-4444-444444444443"), new Guid("88888888-8888-8888-8888-888888888881"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automated shrink-wrap tunnel", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A22", "WS-PKG1-03", 3 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("88888888-8888-8888-8888-888888888881"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Parcel integrity check", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A23", "WS-PKG1-04", 4 },
                    { new Guid("44444444-4444-4444-4444-444444444445"), new Guid("88888888-8888-8888-8888-888888888881"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sorting conveyor loader", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A24", "WS-PKG1-05", 5 },
                    { new Guid("50000000-aaaa-bbbb-cccc-111111111111"), new Guid("22222222-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reflow oven #1", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E1", "WS-RF-01", 1 },
                    { new Guid("50000000-aaaa-bbbb-cccc-111111111112"), new Guid("22222222-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reflow oven #2", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E2", "WS-RF-02", 2 },
                    { new Guid("50000000-aaaa-bbbb-cccc-111111111113"), new Guid("22222222-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reflow oven #3", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E3", "WS-RF-03", 3 },
                    { new Guid("50000000-aaaa-bbbb-cccc-111111111114"), new Guid("22222222-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reflow oven #4", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E4", "WS-RF-04", 4 },
                    { new Guid("50000000-aaaa-bbbb-cccc-111111111115"), new Guid("22222222-bbbb-cccc-dddd-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reflow oven #5", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E5", "WS-RF-05", 5 },
                    { new Guid("55555555-5555-5555-5555-555555555551"), new Guid("88888888-8888-8888-8888-888888888882"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tape dispensing system", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A25", "WS-PKG2-01", 1 },
                    { new Guid("55555555-5555-5555-5555-555555555552"), new Guid("88888888-8888-8888-8888-888888888882"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automated barcode printer", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A26", "WS-PKG2-02", 2 },
                    { new Guid("55555555-5555-5555-5555-555555555553"), new Guid("88888888-8888-8888-8888-888888888882"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Package photo capture unit", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A27", "WS-PKG2-03", 3 },
                    { new Guid("55555555-5555-5555-5555-555555555554"), new Guid("88888888-8888-8888-8888-888888888882"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Load distribution analyzer", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A28", "WS-PKG2-04", 4 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("88888888-8888-8888-8888-888888888882"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outgoing product staging", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A29", "WS-PKG2-05", 5 },
                    { new Guid("60000000-aaaa-bbbb-cccc-111111111111"), new Guid("22222222-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AOI scanner #1", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "F1", "WS-AOI-01", 1 },
                    { new Guid("60000000-aaaa-bbbb-cccc-111111111112"), new Guid("22222222-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AOI scanner #2", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "F2", "WS-AOI-02", 2 },
                    { new Guid("60000000-aaaa-bbbb-cccc-111111111113"), new Guid("22222222-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AOI scanner #3", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "F3", "WS-AOI-03", 3 },
                    { new Guid("60000000-aaaa-bbbb-cccc-111111111114"), new Guid("22222222-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AOI scanner #4", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "F4", "WS-AOI-04", 4 },
                    { new Guid("60000000-aaaa-bbbb-cccc-111111111115"), new Guid("22222222-cccc-dddd-eeee-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AOI scanner #5", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "F5", "WS-AOI-05", 5 },
                    { new Guid("66666666-6666-6666-6666-666666666661"), new Guid("88888888-8888-8888-8888-888888888883"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Box folding robot arm", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A30", "WS-PKG3-01", 1 },
                    { new Guid("66666666-6666-6666-6666-666666666662"), new Guid("88888888-8888-8888-8888-888888888883"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Label placement verification", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A31", "WS-PKG3-02", 2 },
                    { new Guid("66666666-6666-6666-6666-666666666663"), new Guid("88888888-8888-8888-8888-888888888883"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Box pressure test unit", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A32", "WS-PKG3-03", 3 },
                    { new Guid("66666666-6666-6666-6666-666666666664"), new Guid("88888888-8888-8888-8888-888888888883"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automated strapping machine", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A33", "WS-PKG3-04", 4 },
                    { new Guid("66666666-6666-6666-6666-666666666665"), new Guid("88888888-8888-8888-8888-888888888883"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final pallet loading spot", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A34", "WS-PKG3-05", 5 },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manual soldering station", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A1", "WS-SLD-01", 1 },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automated soldering robot arm", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A2", "WS-SLD-02", 2 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Visual inspection camera station", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A8", "WS-INS-01", 1 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Box assembly station", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A14", "WS-PKG-01", 1 }
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
                name: "IX_Simulations_ProductionLineId",
                schema: "cell_tracker",
                table: "Simulations",
                column: "ProductionLineId");

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
                name: "OperatorTasks",
                schema: "cell_tracker");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "cell_tracker");

            migrationBuilder.DropTable(
                name: "Simulations",
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
        }
    }
}
