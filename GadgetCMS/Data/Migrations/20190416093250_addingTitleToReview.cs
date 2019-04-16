using Microsoft.EntityFrameworkCore.Migrations;

namespace GadgetCMS.Data.Migrations
{
    public partial class addingTitleToReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewTitle",
                table: "Review",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "Featured",
                table: "Article",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "ArticleVisible",
                table: "Article",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ArticleEditLock",
                table: "Article",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewTitle",
                table: "Review");

            migrationBuilder.AlterColumn<bool>(
                name: "Featured",
                table: "Article",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "ArticleVisible",
                table: "Article",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ArticleEditLock",
                table: "Article",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }
    }
}
