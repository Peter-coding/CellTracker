using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellTracker.Api.App
{
    /// <inheritdoc />
    public partial class SimulationModel2 : Migration
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
                value: "a4976a46-79d5-40ab-9af6-e2458b401c7d");

            migrationBuilder.UpdateData(
                schema: "cell_tracker",
                table: "SiteUser",
                keyColumn: "Id",
                keyValue: "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5",
                column: "ConcurrencyStamp",
                value: "de329117-5055-40cb-a42b-8d416f469625");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_ProductionLineId",
                schema: "cell_tracker",
                table: "Simulations",
                column: "ProductionLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Simulations_ProductionLines_ProductionLineId",
                schema: "cell_tracker",
                table: "Simulations",
                column: "ProductionLineId",
                principalSchema: "cell_tracker",
                principalTable: "ProductionLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Simulations_ProductionLines_ProductionLineId",
                schema: "cell_tracker",
                table: "Simulations");

            migrationBuilder.DropIndex(
                name: "IX_Simulations_ProductionLineId",
                schema: "cell_tracker",
                table: "Simulations");

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
    }
}
