using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CellTracker.Api.Migrations.App
{
    /// <inheritdoc />
    public partial class Add_DbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cell_tracker");

            migrationBuilder.CreateTable(
                name: "OperatorTasks",
                schema: "cell_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    QuantityGoal = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorTasks", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperatorTasks",
                schema: "cell_tracker");
        }
    }
}
