using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReviewArticleRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Articles_ArticleId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Review_ArticleId",
                table: "Reviews",
                newName: "IX_Reviews_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Articles_ArticleId",
                table: "Reviews",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Articles_ArticleId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ArticleId",
                table: "Review",
                newName: "IX_Review_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Articles_ArticleId",
                table: "Review",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
