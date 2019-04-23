using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Data;
using GadgetCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetCMS.Areas.Dashboard.Pages
{
    [Authorize(Roles= "Admin,Moderator,Editor")]
    public class IndexModel : PageModel
    {
        private readonly GadgetCMSContext _context;
        private readonly ApplicationDbContext _applicationDbContext;

        public IndexModel(GadgetCMSContext context, ApplicationDbContext applicationDbContext)
        {
            _context = context;
            _applicationDbContext = applicationDbContext;
        }

        public List<ArticleData> ArticleDates { get; set; }
        public List<ReviewData> ReviewDates { get; set; }
        public List<CategoryData> CategoryDatas { get; set; }
        public List<RevCreateData> RevCreateDatas { get; set; }

        public IActionResult OnGet()
        {
            var tempArticles = _context.Article.Where(a => DateTime.Compare(a.ArticleCreated, DateTime.Today.AddMonths(-4)) >= 0).GroupBy(a => a.ArticleCreated).Select(g => new { g.Key, Count = g.Count() });

            ArticleDates = new List<ArticleData>();
            foreach (var item in tempArticles)
            {
                var temp = (Int32)(item.Key.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                ArticleDates.Add(
                    new ArticleData
                    {
                        ArtDate = temp.ToString(),
                        ArtCount = item.Count
                    }
                    );
            }

            var tempReviews = _context.Review.Where(a => DateTime.Compare(a.ReviewCreated, DateTime.Today.AddMonths(-4)) >= 0).GroupBy(a => a.ReviewCreated).Select(g => new { g.Key, Count = g.Count() });


            ReviewDates = new List<ReviewData>();
            foreach (var item in tempReviews)
            {
                var temp = (Int32)(item.Key.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                ReviewDates.Add(
                    new ReviewData
                    {
                        RevDate = temp.ToString(),
                        RevCount = item.Count
                    }
                    );
            }

            var tempCategory = _applicationDbContext.Article.GroupBy(a => a.Category.CategoryName).Select(g => new { g.Key, Count = g.Count() });

            CategoryDatas = new List<CategoryData>();
            foreach (var item in tempCategory)
            {
                CategoryDatas.Add(
                    new CategoryData
                    {
                        CatName = item.Key,
                        CatCount = item.Count
                    }
                    );
            }

            var tempRevCreate = _context.Review
                        .Where(a => DateTime.Compare(a.ReviewCreated, DateTime.Today.AddMonths(-4)) >= 0)
                        .GroupBy(o => o.ReviewCreated.DayOfWeek.ToString())
                        .Select(g => new { DayOfWeek = g.Key, Count = g.Count() })
                        .ToList();

            RevCreateDatas = new List<RevCreateData>();
            foreach (var item in tempRevCreate)
            {
                RevCreateDatas.Add(
                    new RevCreateData
                    {
                        RevCreateDoW = item.DayOfWeek,
                        RevCreateCount = item.Count
                    }
                    );
            }

            return Page();
        }
    }

    public class ArticleData
    {
        public string ArtDate { get; set; }
        public int ArtCount { get; set; }
    }

    public class ReviewData
    {
        public string RevDate { get; set; }
        public int RevCount { get; set; }
    }
    public class CategoryData
    {
        public string CatName { get; set; }
        public int CatCount { get; set; }
    }

    public class RevCreateData
    {
        public string RevCreateDoW { get; set; }
        public int RevCreateCount { get; set; }
    }
}