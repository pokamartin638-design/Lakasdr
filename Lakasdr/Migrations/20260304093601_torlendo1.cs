using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lakasdr.Migrations
{
    /// <inheritdoc />
    public partial class torlendo1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 3, 5, "Nagy Milán", 6 },
                    { 4, 7, "Tamás András", 3 },
                    { 5, 2, "Póka Andrea", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
