using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HamburgerAppV1.Data.Migrations
{
    /// <inheritdoc />
    public partial class Iki : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EkstraMalzemeler",
                columns: new[] { "Id", "EktraMalzemeAd", "EktraMalzemeFiyat" },
                values: new object[,]
                {
                    { 1, "Ketçap", 2m },
                    { 2, "Mayonez", 2m },
                    { 3, "Ranch Sos", 4m },
                    { 4, "Buffalo Sos", 4m },
                    { 5, "Barbekü Sos", 4m }
                });

            migrationBuilder.InsertData(
                table: "Menuler",
                columns: new[] { "Id", "MenuAd", "MenuFiyat", "ResimYolu" },
                values: new object[,]
                {
                    { 1, "Cheeseburger Menü", 100m, "cheeseburger-menu" },
                    { 2, "BigKing Menü", 120m, "big-king-menu" },
                    { 3, "King Chicken Menü", 110m, "king-chicken-menu" },
                    { 4, "Whopper Menü", 115m, "whopper-menu" },
                    { 5, "Köfteburger Menü", 90m, "kofteburger-menu" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EkstraMalzemeler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EkstraMalzemeler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EkstraMalzemeler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EkstraMalzemeler",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EkstraMalzemeler",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
