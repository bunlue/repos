using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorMovieWA.Server.Migrations
{
    public partial class RemoveExtraColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_GenreID",
                table: "MovieGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenres",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "GenresID",
                table: "MovieGenres");

            migrationBuilder.AlterColumn<int>(
                name: "GenreID",
                table: "MovieGenres",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenres",
                table: "MovieGenres",
                columns: new[] { "MovieID", "GenreID" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genres_GenreID",
                table: "MovieGenres",
                column: "GenreID",
                principalTable: "Genres",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_GenreID",
                table: "MovieGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenres",
                table: "MovieGenres");

            migrationBuilder.AlterColumn<int>(
                name: "GenreID",
                table: "MovieGenres",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "GenresID",
                table: "MovieGenres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenres",
                table: "MovieGenres",
                columns: new[] { "MovieID", "GenresID" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genres_GenreID",
                table: "MovieGenres",
                column: "GenreID",
                principalTable: "Genres",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
