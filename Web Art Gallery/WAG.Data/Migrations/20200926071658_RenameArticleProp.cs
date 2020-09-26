using Microsoft.EntityFrameworkCore.Migrations;

namespace WAG.Data.Migrations
{
    public partial class RenameArticleProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArticleContentFileName",
                table: "Articles",
                newName: "ArticleContent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArticleContent",
                table: "Articles",
                newName: "ArticleContentFileName");
        }
    }
}
