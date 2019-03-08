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
                .Property(c => c.BanStatus)
                .HasConversion(new BoolToZeroOneConverter<short>());

            modelBuilder.Entity<GadgetCMSUser>()
                .Property(c => c.StarReview)
                .HasDefaultValue(false);

            modelBuilder.Entity<GadgetCMSUser>()
                .Property(c => c.StarReview)
                .HasConversion(new BoolToZeroOneConverter<short>());

            //ArticleLog
            modelBuilder.Entity<ArticleLog>()
                .HasKey(c => new { c.UserId, c.ArticleId });

            //Review
            modelBuilder.Entity<Review>()
                .HasKey(c => new { c.UserId, c.ArticleId });
        }
    }
}
