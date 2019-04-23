using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Areas.Dashboard.Pages;
using GadgetCMS.Areas.Identity.Data;
using GadgetCMS.Data;
using GadgetCMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GadgetCMS.Areas.Identity.Pages
{
    public class MyReviewsModel : PageModel
    {
        private readonly GadgetCMSContext _context;
        private readonly UserManager<GadgetCMSUser> _userManager;

        public MyReviewsModel(GadgetCMSContext context, UserManager<GadgetCMSUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public PaginatedList<Review> Reviews { get; set; }

        public string NameSort { get; set; }
        public string ArticleSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ArticleSort = sortOrder == "Article" ? "article_desc" : "Article";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var sorter = _context.Review
                .Include(r => r.Article)
                .Include(r => r.GadgetCmsUser)
                .Where(r => r.UserId == user.Id)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                sorter = sorter.Where(s => s.ReviewTitle.ToLower().Contains(searchString)
                                       || s.Article.ArticleName.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sorter = sorter.OrderByDescending(s => s.ReviewTitle);
                    break;
                case "Article":
                    sorter = sorter.OrderBy(s => s.Article.ArticleName);
                    break;
                case "article_desc":
                    sorter = sorter.OrderByDescending(s => s.Article.ArticleName);
                    break;
                case "Date":
                    sorter = sorter.OrderBy(s => s.ReviewCreated);
                    break;
                case "date_desc":
                    sorter = sorter.OrderByDescending(s => s.ReviewCreated);
                    break;
                default:
                    sorter = sorter.OrderBy(s => s.ReviewTitle);
                    break;
            }

            int pageSize = 15;
            Reviews = PaginatedList<Review>.Create(
                sorter.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}