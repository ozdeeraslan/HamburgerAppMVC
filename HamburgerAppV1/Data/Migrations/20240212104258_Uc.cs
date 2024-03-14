using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerAppV1.Data.Migrations
{
    /// <inheritdoc />
    public partial class Uc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 1,
                column: "ResimYolu",
                value: "cheeseburger-menu.png");

            migrationBuilder.UpdateData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 2,
                column: "ResimYolu",
                value: "big-king-menu.png");

            migrationBuilder.UpdateData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 3,
                column: "ResimYolu",
                value: "king-chicken-menu.png");

            migrationBuilder.UpdateData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 4,
                column: "ResimYolu",
                value: "whopper-menu.png");

            migrationBuilder.UpdateData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 5,
                column: "ResimYolu",
                value: "kofteburger-menu.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 1,
                column: "ResimYolu",
                value: "cheeseburger-menu");

            migrationBuilder.UpdateData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 2,
                column: "ResimYolu",
                value: "big-king-menu");

            migrationBuilder.UpdateData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 3,
                column: "ResimYolu",
                value: "king-chicken-menu");

            migrationBuilder.UpdateData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 4,
                column: "ResimYolu",
                value: "whopper-menu");

            migrationBuilder.UpdateData(
                table: "Menuler",
                keyColumn: "Id",
                keyValue: 5,
                column: "ResimYolu",
                value: "kofteburger-menu");
        }
    }
}
