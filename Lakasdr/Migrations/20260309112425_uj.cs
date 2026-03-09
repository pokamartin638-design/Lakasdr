using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lakasdr.Migrations
{
    /// <inheritdoc />
    public partial class uj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ertek = table.Column<int>(type: "int", nullable: false),
                    Ideje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Work",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Works = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: false),
                    Exp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A fürdőszoba felújítása során modern burkolatok, új szaniterek és energiatakarékos világítás került beépítésre, így a helyiség letisztult és időtálló megjelenést kapott.", "Fürdőszoba felújítás" },
                    { 2, "A hálószoba átalakítása friss falfestéssel, meleg hatású padlóburkolattal és egyedi beépített szekrénnyel történt, amely nyugodt, harmonikus légkört teremt.", "Hálószoba felújítás" },
                    { 3, "A konyha felújítása során korszerű konyhabútor, praktikus tárolási megoldások és modern gépek kerültek beépítésre, hogy a főzés kényelmesebb és hatékonyabb legyen.", "Konyha felújítás" },
                    { 4, "A kocsi bejáró térkövezése strapabíró, esztétikus térkövekkel készült, biztosítva a tartós, stabil burkolatot és az igényes megjelenést.", "Kocsi beálló térkövezés" }
                });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "Exp", "Name", "WorkId" },
                values: new object[,]
                {
                    { 1, 3, "Nagy Mátyás", 1 },
                    { 2, 1, "Kis Elek", 2 },
                    { 3, 5, "Nagy Milán", 4 },
                    { 4, 7, "Tamás András", 3 },
                    { 5, 2, "Póka Andrea", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Work");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
