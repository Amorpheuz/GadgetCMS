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
    [Migration("20190308092853_CreateArticleArticleLogReview")]
    partial class CreateArticleArticleLogReview
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
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
                        .HasDefaultValueSql("CONVERT(date, GETDATE())");

                    b.Property<short>("ArticleEditLock")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue((short)0);

                    b.Property<string>("ArticleLastEditedBy")
                        .IsRequired();

                    b.Property<DateTime>("ArticleLastUpdate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ArticleName")
                        .IsRequired();

                    b.Property<short>("ArticleVisible")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue((short)1);

                    b.HasKey("ArticleId");

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

            modelBuilder.Entity("GadgetCMS.Data.Review", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("ArticleId");

                    b.Property<string>("ReviewContent")
                        .IsRequired();

                    b.Property<DateTime>("ReviewCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CONVERT(date, GETDATE())");

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

                    b.Property<short>("BanStatus")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue((short)0);

                    b.Property<string>("Nickname");

                    b.Property<short>("StarReview")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue((short)0);

                    b.HasDiscriminator().HasValue("GadgetCMSUser");
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

            modelBuilder.Entity("GadgetCMS.Data.Review", b =>
                {
                    b.HasOne("GadgetCMS.Data.Article", "Article")
                        .WithMany()
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
