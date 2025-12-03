using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CellTracker.Api.Migrations.App
{
    /// <inheritdoc />
    public partial class OperatorTaskExtended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "35031d70-8287-4bfe-bd63-05a816f44885");

            migrationBuilder.DeleteData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkStationId",
                schema: "cell_tracker",
                table: "OperatorTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OperatorTasks_WorkStationId",
                schema: "cell_tracker",
                table: "OperatorTasks",
                column: "WorkStationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OperatorTasks_WorkStations_WorkStationId",
                schema: "cell_tracker",
                table: "OperatorTasks",
                column: "WorkStationId",
                principalSchema: "cell_tracker",
                principalTable: "WorkStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatorTasks_WorkStations_WorkStationId",
                schema: "cell_tracker",
                table: "OperatorTasks");

            migrationBuilder.DropIndex(
                name: "IX_OperatorTasks_WorkStationId",
                schema: "cell_tracker",
                table: "OperatorTasks");

            migrationBuilder.DropColumn(
                name: "WorkStationId",
                schema: "cell_tracker",
                table: "OperatorTasks");

            migrationBuilder.InsertData(
                schema: "cell_tracker",
                table: "SiteUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "LoginCode", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "35031d70-8287-4bfe-bd63-05a816f44885", 0, "6916c252-7a94-49a5-aa43-7ec194af2a1f", "test.user1@example.com", true, "Test1", "User1", false, null, null, "TEST.USER1@EXAMPLE.COM", "TESTUSER1", "0000000", null, false, "35031d70-8287-4bfe-bd63-05a816f44880", false, "testuser1" },
                    { "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5", 0, "b1dad2a8-50e9-4261-9e6c-860f22920143", "test.user@example.com", true, "Test", "User", false, null, null, "TEST.USER@EXAMPLE.COM", "TESTUSER", "1111111", null, false, "ee6739f0-97e2-496c-8ffe-74a39ae7a8e4", false, "testuser" }
                });
        }
    }
}
