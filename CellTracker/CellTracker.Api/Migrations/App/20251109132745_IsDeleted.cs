using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellTracker.Api.App
{
    /// <inheritdoc />
    public partial class IsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "cell_tracker",
                table: "WorkStations",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "cell_tracker",
                table: "ProductionLines",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "cell_tracker",
                table: "OperatorTasks",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "cell_tracker",
                table: "Factories",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "cell_tracker",
                table: "Cells",
                type: "boolean",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "Cells",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "Cells",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "Cells",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "Factories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "Factories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "35031d70-8287-4bfe-bd63-05a816f44885",
                column: "ConcurrencyStamp",
                value: "c4bef9b2-2c17-42e7-a90e-81702e15f43f");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5",
                column: "ConcurrencyStamp",
                value: "fe2ff245-d389-4c67-814d-06e6a437c5dc");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "cell_tracker",
                table: "WorkStations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "cell_tracker",
                table: "ProductionLines");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "cell_tracker",
                table: "OperatorTasks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "cell_tracker",
                table: "Factories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "cell_tracker",
                table: "Cells");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "35031d70-8287-4bfe-bd63-05a816f44885",
                column: "ConcurrencyStamp",
                value: "494850a4-9f8c-46b2-97d5-f2b406dcc6f3");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5",
                column: "ConcurrencyStamp",
                value: "016a5a34-7e14-4d0f-b1f7-6041fb9d147a");
        }
    }
}
