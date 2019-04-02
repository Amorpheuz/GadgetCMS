using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Areas.Identity.Data;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GadgetCMS.Models
{
    public class GadgetCMSContext : IdentityDbContext<GadgetCMSUser>
    {
        public GadgetCMSContext(DbContextOptions<GadgetCMSContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //GadgetCMSUser
            modelBuilder.Entity<GadgetCMSUser>()
                .Property(c => c.BanStatus)
                .HasDefaultValue(false);

            modelBuilder.Entity<GadgetCMSUser>()
                .Property(c => c.StarReview)
                .HasDefaultValue(false);

            //ArticleLog
            modelBuilder.Entity<ArticleLog>()
                .HasKey(c => new { c.UserId, c.ArticleId });

            //Review
            modelBuilder.Entity<Review>()
                .HasKey(c => new { c.UserId, c.ArticleId });

            //Article
            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleVisible)
                .HasDefaultValue(true);

            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleEditLock)
                .HasDefaultValue(false);

            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleRating)
                .HasDefaultValue(0);

            //Need to set Trigger for LastUpdate on add or update in Database
            modelBuilder.Entity<Article>()
                .Property(c => c.ArticleLastUpdate)
                .ValueGeneratedOnAddOrUpdate();

            //---------------------------------------------------------------------
            //CategoryParentParameter
            modelBuilder.Entity<CategoryParentParameter>()
                .HasKey(c => new { c.CategoryId, c.ParentParameterId });

            //---------------------------------------------------------------------
            //ArticleParameter
            modelBuilder.Entity<ArticleParameter>()
                .HasKey(c => new { c.ArticleId, c.ParameterId });
        }

        public DbSet<GadgetCMS.Data.Review> Review { get; set; }

        public DbSet<GadgetCMS.Data.Article> Article { get; set; }
    }
}
