using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using GadgetCMS.Data;

namespace GadgetCMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //---------------------------------------------------------------------
            //ArticleLog
            modelBuilder.Entity<ArticleLog>()
                .HasKey(c => new { c.UserId, c.ArticleId });
            
            //---------------------------------------------------------------------
            //Article
            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleVisible)
                .HasDefaultValue(true);

            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleVisible)
                .HasConversion(new BoolToZeroOneConverter<short>());

            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleEditLock)
                .HasDefaultValue(false);

            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleEditLock)
                .HasConversion(new BoolToZeroOneConverter<short>());

            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleCreated)
                .HasDefaultValueSql("CONVERT(date, GETDATE())");

            //Need to set Trigger for LastUpdate on add or update in Database
            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleLastUpdate)
                .ValueGeneratedOnAddOrUpdate();

            //---------------------------------------------------------------------
            //GadgetCMSUser
            modelBuilder.Entity<GadgetCMSUser>()
                .Property(c => c.BanStatus)
                .HasDefaultValue(false);

            modelBuilder.Entity<GadgetCMSUser>()
                .Property(c => c.BanStatus)
                .HasConversion(new BoolToZeroOneConverter<short>());

            modelBuilder.Entity<GadgetCMSUser>()
                .Property(c => c.StarReview)
                .HasDefaultValue(false);

            modelBuilder.Entity<GadgetCMSUser>()
                .Property(c => c.StarReview)
                .HasConversion(new BoolToZeroOneConverter<short>());

            //---------------------------------------------------------------------
            //Review
            modelBuilder.Entity<Review>()
                .HasKey(c => new { c.UserId, c.ArticleId });

            modelBuilder.Entity<Review>()
                .Property(c => c.ReviewCreated)
                .HasDefaultValueSql("CONVERT(date, GETDATE())");

            //Need to set Trigger for LastUpdate on add or update in Database
            modelBuilder.Entity<Review>()
                .Property(c => c.ReviewLastUpdate)
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Review>()
                .Property(c => c.ReviewVisible)
                .HasDefaultValue(true);

            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleEditLock)
                .HasConversion(new BoolToZeroOneConverter<short>());
        }

        public DbSet<GadgetCMS.Data.Article> Article { get; set; }

        public DbSet<GadgetCMS.Data.Review> Review { get; set; }

        public DbSet<GadgetCMS.Data.ArticleLog> ArticleLog { get; set; }
    }
}
