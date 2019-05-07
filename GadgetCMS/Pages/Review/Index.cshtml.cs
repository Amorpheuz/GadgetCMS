using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;
using GadgetCMS.Models;
using GadgetCMS.Areas.Dashboard.Pages;

namespace GadgetCMS.Pages.Review
{
    public class IndexModel : PageModel
    {
        private readonly GadgetCMSContext _userContext;

        public IndexModel(GadgetCMSContext userContext)
        {
            _userContext = userContext;
        }

        public PaginatedList<Data.Review> Reviews { get; set; }

        public string NameSort { get; set; }
        public string AuthorSort { get; set; }
        public string ArticleSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(int id,string sortOrder, string currentFilter, string searchString, int? pageIndex)
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

            var sorter = _userContext.Review
                .Include(r => r.Article)
                .Include(r => r.GadgetCmsUser)
                .Where(r => r.ArticleId == id)
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

            ViewData["ArtId"] = id;
            ViewData["PosReviews"] = _userContext.Review.Where(r => r.ReviewType == true && r.ArticleId == id).Count();
            ViewData["NegReviews"] = _userContext.Review.Where(r => r.ReviewType == false && r.ArticleId == id).Count();

            int pageSize = 15;
            Reviews = PaginatedList<Data.Review>.Create(
                sorter.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
