using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SehirId",
                table: "Doktorlar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UlkeId",
                table: "Doktorlar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_SehirId",
                table: "Doktorlar",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_UlkeId",
                table: "Doktorlar",
                column: "UlkeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doktorlar_Sehirler_SehirId",
                table: "Doktorlar",
                column: "SehirId",
                principalTable: "Sehirler",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doktorlar_Ulkeler_UlkeId",
                table: "Doktorlar",
                column: "UlkeId",
                principalTable: "Ulkeler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doktorlar_Sehirler_SehirId",
                table: "Doktorlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Doktorlar_Ulkeler_UlkeId",
                table: "Doktorlar");

            migrationBuilder.DropIndex(
                name: "IX_Doktorlar_SehirId",
                table: "Doktorlar");

            migrationBuilder.DropIndex(
                name: "IX_Doktorlar_UlkeId",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "SehirId",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "UlkeId",
                table: "Doktorlar");
        }
    }
}
