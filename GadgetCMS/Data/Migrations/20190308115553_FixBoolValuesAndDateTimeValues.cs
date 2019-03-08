using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GadgetCMS.Data.Migrations
{
    public partial class FixBoolValuesAndDateTimeValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReviewCreated",
                table: "Review",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "CONVERT(date, GETDATE())");

            migrationBuilder.AlterColumn<bool>(
                name: "StarReview",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(short),
                oldNullable: true,
                oldDefaultValue: (short)0);

            migrationBuilder.AlterColumn<bool>(
                name: "BanStatus",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(short),
                oldNullable: true,
                oldDefaultValue: (short)0);

            migrationBuilder.AlterColumn<bool>(
                name: "ArticleVisible",
                table: "Article",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(short),
                oldDefaultValue: (short)1);

            migrationBuilder.AlterColumn<bool>(
                name: "ArticleEditLock",
                table: "Article",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(short),
                oldDefaultValue: (short)0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArticleCreated",
                table: "Article",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "CONVERT(date, GETDATE())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReviewCreated",
                table: "Review",
                nullable: false,
                defaultValueSql: "CONVERT(date, GETDATE())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<short>(
                name: "StarReview",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: (short)0,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<short>(
                name: "BanStatus",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: (short)0,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<short>(
                name: "ArticleVisible",
                table: "Article",
                nullable: false,
                defaultValue: (short)1,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<short>(
                name: "ArticleEditLock",
                table: "Article",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArticleCreated",
                table: "Article",
                nullable: false,
                defaultValueSql: "CONVERT(date, GETDATE())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
