using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CellTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class IdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");

            migrationBuilder.Sql("ALTER TABLE \"OperatorTasks\" ADD COLUMN \"Id_new\" uuid DEFAULT uuid_generate_v4();");

            migrationBuilder.Sql("UPDATE \"OperatorTasks\" SET \"Id_new\" = uuid_generate_v4();");
  
            migrationBuilder.Sql("ALTER TABLE \"OperatorTasks\" DROP COLUMN \"Id\";");

            migrationBuilder.Sql("ALTER TABLE \"OperatorTasks\" RENAME COLUMN \"Id_new\" TO \"Id\";");

            migrationBuilder.DeleteData(
                table: "OperatorTasks",
                keyColumn: "Id",
                keyValue: new Guid("59c63fad-d4e5-477d-a1fc-a8a8b8fca6ad"));

            migrationBuilder.DeleteData(
                table: "OperatorTasks",
                keyColumn: "Id",
                keyValue: new Guid("a8ed6933-3ba3-46d6-9504-182a49b572cf"));

            migrationBuilder.InsertData(
                table: "OperatorTasks",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "QuantityGoal" },
                values: new object[,]
                {
                    { new Guid("03efcf35-2691-4620-9a0d-21c7a213f94f"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sample operator task", "Test Task", 10 },
                    { new Guid("5ca9488f-69ba-4e16-9d95-d4539c3667df"), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Sample operator task2", "Test Task2", 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperatorTasks",
                keyColumn: "Id",
                keyValue: new Guid("03efcf35-2691-4620-9a0d-21c7a213f94f"));

            migrationBuilder.DeleteData(
                table: "OperatorTasks",
                keyColumn: "Id",
                keyValue: new Guid("5ca9488f-69ba-4e16-9d95-d4539c3667df"));

            migrationBuilder.InsertData(
                table: "OperatorTasks",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "QuantityGoal" },
                values: new object[,]
                {
                    { new Guid("59c63fad-d4e5-477d-a1fc-a8a8b8fca6ad"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sample operator task", "Test Task", 10 },
                    { new Guid("a8ed6933-3ba3-46d6-9504-182a49b572cf"), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Sample operator task2", "Test Task2", 15 }
                });
        }
    }
}
