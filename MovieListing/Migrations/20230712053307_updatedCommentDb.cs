using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieListing.Migrations
{
    public partial class updatedCommentDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_ApplicationUserUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Movies_MoviesId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_MoviesId",
                table: "Comments",
                newName: "IX_Comments_MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ApplicationUserUserId",
                table: "Comments",
                newName: "IX_Comments_ApplicationUserUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserUserId",
                table: "Comments",
                column: "ApplicationUserUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Movies_MoviesId",
                table: "Comments",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Movies_MoviesId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_MoviesId",
                table: "Comment",
                newName: "IX_Comment_MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ApplicationUserUserId",
                table: "Comment",
                newName: "IX_Comment_ApplicationUserUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_ApplicationUserUserId",
                table: "Comment",
                column: "ApplicationUserUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Movies_MoviesId",
                table: "Comment",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
