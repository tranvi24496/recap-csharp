using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoGameManager.Migrations
{
    public partial class AddForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Genres_GenreId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Games",
                newName: "GameGenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_GenreId",
                table: "Games",
                newName: "IX_Games_GameGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Genres_GameGenreId",
                table: "Games",
                column: "GameGenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Genres_GameGenreId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "GameGenreId",
                table: "Games",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_GameGenreId",
                table: "Games",
                newName: "IX_Games_GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Genres_GenreId",
                table: "Games",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
