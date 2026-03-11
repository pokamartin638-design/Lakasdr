using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lakasdr.Migrations
{
    /// <inheritdoc />
    public partial class ujRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Desc", "Email", "Ertek", "Ideje" },
                values: new object[] { 1, "fasza", "toth.mark0603@gmail.com", 4, new DateTime(2026, 3, 11, 10, 59, 52, 373, DateTimeKind.Local).AddTicks(9789) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
