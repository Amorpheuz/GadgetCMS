using Microsoft.EntityFrameworkCore.Migrations;

namespace GadgetCMS.Data.Migrations
{
    public partial class AddedArticleSubtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleSubtitle",
                table: "Article",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleSubtitle",
                table: "Article");
        }
    }
}
