using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CellTracker.Api.Migrations.Identity
{
    /// <inheritdoc />
    public partial class addRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Token = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35031d70-8287-4bfe-bd63-05a816f44885",
                column: "ConcurrencyStamp",
                value: "228524b1-3286-46c1-b57b-ea9cccb0583b");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5",
                column: "ConcurrencyStamp",
                value: "3934e15f-df75-4034-b754-0a586140c2da");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                schema: "identity",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "identity",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "identity");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35031d70-8287-4bfe-bd63-05a816f44885",
                column: "ConcurrencyStamp",
                value: "498914e2-b3e0-4db4-b8e5-10256a1f2183");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ee6739f0-97e2-496c-8ffe-74a39ae7a8e5",
                column: "ConcurrencyStamp",
                value: "3dbd16f4-05d3-405e-aba7-a8fb66db539d");
        }
    }
}
