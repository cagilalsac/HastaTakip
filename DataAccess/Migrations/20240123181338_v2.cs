using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doktorlar_Klinik_KlinikId",
                table: "Doktorlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klinik",
                table: "Klinik");

            migrationBuilder.RenameTable(
                name: "Klinik",
                newName: "Klinikler");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klinikler",
                table: "Klinikler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doktorlar_Klinikler_KlinikId",
                table: "Doktorlar",
                column: "KlinikId",
                principalTable: "Klinikler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doktorlar_Klinikler_KlinikId",
                table: "Doktorlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klinikler",
                table: "Klinikler");

            migrationBuilder.RenameTable(
                name: "Klinikler",
                newName: "Klinik");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klinik",
                table: "Klinik",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doktorlar_Klinik_KlinikId",
                table: "Doktorlar",
                column: "KlinikId",
                principalTable: "Klinik",
                principalColumn: "Id");
        }
    }
}
