using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieListing.Migrations
{
    public partial class removedfkgenremovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_GenreRefId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieRefId",
                table: "Genre");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreRefId",
                table: "Movies",
                column: "GenreRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_GenreRefId",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "MovieRefId",
                table: "Genre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreRefId",
                table: "Movies",
                column: "GenreRefId",
                unique: true);
        }
    }
}
