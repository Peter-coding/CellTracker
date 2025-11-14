using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellTracker.Api.App
{
    /// <inheritdoc />
    public partial class SimulationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "35031d70-8287-4bfe-bd63-05a816f44885",
                column: "ConcurrencyStamp",
                value: "66a4f693-00f2-4812-880a-7ced3ffa8e21");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5",
                column: "ConcurrencyStamp",
                value: "2b8838ca-5d64-49b8-8531-8f2add1237e8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Simulations",
                schema: "cell_tracker");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "35031d70-8287-4bfe-bd63-05a816f44885",
                column: "ConcurrencyStamp",
                value: "328160e5-ac01-449d-b5ac-c829072a6ec7");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5",
                column: "ConcurrencyStamp",
                value: "7194e626-8170-4aef-9730-b2ca035794f2");
        }
    }
}
