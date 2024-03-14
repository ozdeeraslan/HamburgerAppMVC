using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerAppV1.Data.Migrations
{
    /// <inheritdoc />
    public partial class sekiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedEkstraMalzemeler",
                table: "Siparisler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectedEkstraMalzemeler",
                table: "Siparisler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
