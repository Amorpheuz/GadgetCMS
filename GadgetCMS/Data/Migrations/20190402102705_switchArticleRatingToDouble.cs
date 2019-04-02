using Microsoft.EntityFrameworkCore.Migrations;

namespace GadgetCMS.Data.Migrations
{
    public partial class switchArticleRatingToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ArticleRating",
                table: "Article",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ArticleRating",
                table: "Article",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldDefaultValue: 0.0);
        }
    }
}
