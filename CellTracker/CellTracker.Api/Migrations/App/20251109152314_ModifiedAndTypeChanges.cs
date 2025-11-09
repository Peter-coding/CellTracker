using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellTracker.Api.App
{
    /// <inheritdoc />
    public partial class ModifiedAndTypeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
