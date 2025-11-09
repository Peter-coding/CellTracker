using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellTracker.Api.App
{
    /// <inheritdoc />
    public partial class Dtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrdinalNumber",
                schema: "cell_tracker",
                table: "WorkStations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "cell_tracker",
                table: "WorkStations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "OrdinalNumber",
                schema: "cell_tracker",
                table: "ProductionLines",
                type: "integer",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "cell_tracker",
                table: "ProductionLines",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "cell_tracker",
                table: "OperatorTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "cell_tracker",
                table: "Factories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "cell_tracker",
                table: "Cells",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "Cells",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "ModifiedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "Cells",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "ModifiedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "Cells",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "ModifiedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "Factories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "ModifiedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "Factories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "ModifiedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "ModifiedAt", "OrdinalNumber" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "ModifiedAt", "OrdinalNumber" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "ModifiedAt", "OrdinalNumber" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "35031d70-8287-4bfe-bd63-05a816f44885",
                column: "ConcurrencyStamp",
                value: "b1a69d46-c6e2-4121-bd53-e6bc0f8dde9e");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5",
                column: "ConcurrencyStamp",
                value: "c6ef80fc-c7c4-4aac-96fc-74ea9a8042ab");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "ModifiedAt", "OrdinalNumber" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ModifiedAt", "OrdinalNumber" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ModifiedAt", "OrdinalNumber" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ModifiedAt", "OrdinalNumber" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "cell_tracker",
                table: "WorkStations");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "cell_tracker",
                table: "ProductionLines");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "cell_tracker",
                table: "OperatorTasks");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "cell_tracker",
                table: "Factories");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "cell_tracker",
                table: "Cells");

            migrationBuilder.AlterColumn<short>(
                name: "OrdinalNumber",
                schema: "cell_tracker",
                table: "WorkStations",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<short>(
                name: "OrdinalNumber",
                schema: "cell_tracker",
                table: "ProductionLines",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "OrdinalNumber",
                value: (short)1);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "OrdinalNumber",
                value: (short)2);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "ProductionLines",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "OrdinalNumber",
                value: (short)1);

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
                column: "OrdinalNumber",
                value: (short)1);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "OrdinalNumber",
                value: (short)2);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "OrdinalNumber",
                value: (short)1);

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "WorkStations",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "OrdinalNumber",
                value: (short)1);
        }
    }
}
