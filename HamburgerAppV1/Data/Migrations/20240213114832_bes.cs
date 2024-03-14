using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerAppV1.Data.Migrations
{
    /// <inheritdoc />
    public partial class bes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Siparisler_AspNetUsers_KullaniciId",
                table: "Siparisler");

            migrationBuilder.RenameColumn(
                name: "KullaniciId",
                table: "Siparisler",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Siparisler_KullaniciId",
                table: "Siparisler",
                newName: "IX_Siparisler_IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Siparisler_AspNetUsers_IdentityUserId",
                table: "Siparisler",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Siparisler_AspNetUsers_IdentityUserId",
                table: "Siparisler");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Siparisler",
                newName: "KullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_Siparisler_IdentityUserId",
                table: "Siparisler",
                newName: "IX_Siparisler_KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Siparisler_AspNetUsers_KullaniciId",
                table: "Siparisler",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
