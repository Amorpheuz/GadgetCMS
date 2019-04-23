using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Models;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GadgetCMS.Areas.Dashboard.Pages
{
    public class ReviewsModel : PageModel
    {
        private readonly GadgetCMSContext _context;

        public ReviewsModel(GadgetCMSContext context)
        {
            _context = context;
        }

        public PaginatedList<Review> Reviews { get; set; }

        public string NameSort { get; set; }
        public string AuthorSort { get; set; }
        public string ArticleSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";
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

            var sorter = _context.Review
                .Include(r => r.Article)
                .Include(r => r.GadgetCmsUser)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                sorter = sorter.Where(s => s.ReviewTitle.ToLower().Contains(searchString)
                                       || s.GadgetCmsUser.Email.ToLower().Contains(searchString)
                                       || s.Article.ArticleName.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sorter = sorter.OrderByDescending(s => s.ReviewTitle);
                    break;
                case "Author":
                    sorter = sorter.OrderBy(s => s.GadgetCmsUser.Email);
                    break;
                case "author_desc":
                    sorter = sorter.OrderByDescending(s => s.GadgetCmsUser.Email);
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