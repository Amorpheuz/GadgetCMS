using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GadgetCMS.Data.Migrations
{
    public partial class AddingCategoriesParametersPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Article",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArticlePicture",
                columns: table => new
                {
                    ArticlePictureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticlePictureBytes = table.Column<byte[]>(nullable: false),
                    ArticlePictureCaption = table.Column<string>(nullable: false),
                    ArticleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlePicture", x => x.ArticlePictureId);
                    table.ForeignKey(
                        name: "FK_ArticlePicture_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: false),
                    CategoryDescription = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ParentParameter",
                columns: table => new
                {
                    ParentParameterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentParameterName = table.Column<string>(nullable: false),
                    ParentParameterDescription = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentParameter", x => x.ParentParameterId);
                });

            migrationBuilder.CreateTable(
                name: "CategoryParentParameter",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    ParentParameterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryParentParameter", x => new { x.CategoryId, x.ParentParameterId });
                    table.ForeignKey(
                        name: "FK_CategoryParentParameter_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryParentParameter_ParentParameter_ParentParameterId",
                        column: x => x.ParentParameterId,
                        principalTable: "ParentParameter",
                        principalColumn: "ParentParameterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parameter",
                columns: table => new
                {
                    ParameterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParameterName = table.Column<string>(nullable: false),
                    ParameterDescription = table.Column<string>(nullable: false),
                    ParameterUnit = table.Column<string>(nullable: false),
                    ParentParameterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.ParameterId);
                    table.ForeignKey(
                        name: "FK_Parameter_ParentParameter_ParentParameterId",
                        column: x => x.ParentParameterId,
                        principalTable: "ParentParameter",
                        principalColumn: "ParentParameterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleParameter",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false),
                    ParameterId = table.Column<int>(nullable: false),
                    ParameterVal = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleParameter", x => new { x.ArticleId, x.ParameterId });
                    table.ForeignKey(
                        name: "FK_ArticleParameter_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleParameter_Parameter_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "Parameter",
                        principalColumn: "ParameterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_CategoryId",
                table: "Article",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleParameter_ParameterId",
                table: "ArticleParameter",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePicture_ArticleId",
                table: "ArticlePicture",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryParentParameter_ParentParameterId",
                table: "CategoryParentParameter",
                column: "ParentParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_ParentParameterId",
                table: "Parameter",
                column: "ParentParameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Category_CategoryId",
                table: "Article",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Category_CategoryId",
                table: "Article");

            migrationBuilder.DropTable(
                name: "ArticleParameter");

            migrationBuilder.DropTable(
                name: "ArticlePicture");

            migrationBuilder.DropTable(
                name: "CategoryParentParameter");

            migrationBuilder.DropTable(
                name: "Parameter");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ParentParameter");

            migrationBuilder.DropIndex(
                name: "IX_Article_CategoryId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Article");
        }
    }
}
