using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoGameManager.Migrations
{
    public partial class HandleDeleteForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Genres_GameGenreId",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Genres_GameGenreId",
                table: "Games",
                column: "GameGenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Genres_GameGenreId",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Genres_GameGenreId",
                table: "Games",
                column: "GameGenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
