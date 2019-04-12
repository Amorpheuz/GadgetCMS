﻿// <auto-generated />
using System;
using GadgetCMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GadgetCMS.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190412075451_AddedArticleSubtitle")]
    partial class AddedArticleSubtitle
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GadgetCMS.Data.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArticleAuthor")
                        .IsRequired();

                    b.Property<string>("ArticleContent")
                        .IsRequired();

                    b.Property<DateTime>("ArticleCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool?>("ArticleEditLock")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("ArticleLastEditedBy")
                        .IsRequired();

                    b.Property<DateTime>("ArticleLastUpdate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ArticleName")
                        .IsRequired();

                    b.Property<double>("ArticleRating")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0.0);

                    b.Property<string>("ArticleSubtitle")
                        .IsRequired();

                    b.Property<bool?>("ArticleVisible")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int>("CategoryId");

                    b.Property<bool?>("Featured")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("ArticleId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("GadgetCMS.Data.ArticleLog", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("ArticleId");

                    b.Property<string>("ArticleLogContent")
                        .IsRequired();

                    b.Property<string>("ArticleLogType")
                        .IsRequired();

                    b.HasKey("UserId", "ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("ArticleLog");
                });

            modelBuilder.Entity("GadgetCMS.Data.ArticleParameter", b =>
                {
                    b.Property<int>("ArticleId");

                    b.Property<int>("ParameterId");

                    b.Property<string>("ParameterVal")
                        .IsRequired();

                    b.HasKey("ArticleId", "ParameterId");

                    b.HasIndex("ParameterId");

                    b.ToTable("ArticleParameter");
                });

            modelBuilder.Entity("GadgetCMS.Data.ArticlePicture", b =>
                {
                    b.Property<int>("ArticlePictureId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId");

                    b.Property<byte[]>("ArticlePictureBytes");

                    b.Property<string>("ArticlePictureCaption")
                        .IsRequired();

                    b.HasKey("ArticlePictureId");

                    b.HasIndex("ArticleId");

                    b.ToTable("ArticlePicture");
                });

            modelBuilder.Entity("GadgetCMS.Data.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryDescription")
                        .IsRequired();

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("GadgetCMS.Data.CategoryParentParameter", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("ParentParameterId");

                    b.HasKey("CategoryId", "ParentParameterId");

                    b.HasIndex("ParentParameterId");

                    b.ToTable("CategoryParentParameter");
                });

            modelBuilder.Entity("GadgetCMS.Data.Parameter", b =>
                {
                    b.Property<int>("ParameterId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ParameterDescription")
                        .IsRequired();

                    b.Property<string>("ParameterName")
                        .IsRequired();

                    b.Property<string>("ParameterUnit")
                        .IsRequired();

                    b.Property<int>("ParentParameterId");

                    b.HasKey("ParameterId");

                    b.HasIndex("ParentParameterId");

                    b.ToTable("Parameter");
                });

            modelBuilder.Entity("GadgetCMS.Data.ParentParameter", b =>
                {
                    b.Property<int>("ParentParameterId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ParentParameterDescription")
                        .IsRequired();

                    b.Property<string>("ParentParameterName")
                        .IsRequired();

                    b.HasKey("ParentParameterId");

                    b.ToTable("ParentParameter");
                });

            modelBuilder.Entity("GadgetCMS.Data.Review", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("ArticleId");

                    b.Property<string>("ReviewContent")
                        .IsRequired();

                    b.Property<DateTime>("ReviewCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("ReviewLastUpdate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<double>("ReviewRating");

                    b.Property<bool>("ReviewVisible")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.HasKey("UserId", "ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GadgetCMS.Areas.Identity.Data.GadgetCMSUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<bool>("BanStatus")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Nickname");

                    b.Property<bool>("StarReview")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasDiscriminator().HasValue("GadgetCMSUser");
                });

            modelBuilder.Entity("GadgetCMS.Data.Article", b =>
                {
                    b.HasOne("GadgetCMS.Data.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GadgetCMS.Data.ArticleLog", b =>
                {
                    b.HasOne("GadgetCMS.Data.Article", "Article")
                        .WithMany("ArticleLogs")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GadgetCMS.Areas.Identity.Data.GadgetCMSUser", "GadgetCmsUser")
                        .WithMany("ArticleLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GadgetCMS.Data.ArticleParameter", b =>
                {
                    b.HasOne("GadgetCMS.Data.Article", "Article")
                        .WithMany("ArticleParameters")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GadgetCMS.Data.Parameter", "Parameter")
                        .WithMany("ArticleParameters")
                        .HasForeignKey("ParameterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GadgetCMS.Data.ArticlePicture", b =>
                {
                    b.HasOne("GadgetCMS.Data.Article", "Article")
                        .WithMany("ArticlePictures")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GadgetCMS.Data.CategoryParentParameter", b =>
                {
                    b.HasOne("GadgetCMS.Data.Category", "Category")
                        .WithMany("CategoryParentParameters")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GadgetCMS.Data.ParentParameter", "ParentParameter")
                        .WithMany("CategoryParentParameters")
                        .HasForeignKey("ParentParameterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GadgetCMS.Data.Parameter", b =>
                {
                    b.HasOne("GadgetCMS.Data.ParentParameter", "ParentParameter")
                        .WithMany("Parameters")
                        .HasForeignKey("ParentParameterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GadgetCMS.Data.Review", b =>
                {
                    b.HasOne("GadgetCMS.Data.Article", "Article")
                        .WithMany("Reviews")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GadgetCMS.Areas.Identity.Data.GadgetCMSUser", "GadgetCmsUser")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
