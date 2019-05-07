using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GadgetCMS.Areas.Dashboard.Pages
{
    [Authorize(Roles = "Admin,Moderator,Editor")]
    public class ArticlesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ArticlesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<Data.Article> Articles { get; set; }

        public string NameSort { get; set; }
        public string AuthorSort { get; set; }
        public string CategorySort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";
            CategorySort = sortOrder == "Category" ? "category_desc" : "Category";
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

            var tempArticles = await _context.Article
                .Include(a => a.Category)
                .Include(a => a.ArticlePictures)
                .ToListAsync();

            var sorter = tempArticles.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                sorter = sorter.Where(s => s.ArticleAuthor.ToLower().Contains(searchString)
                                       || s.ArticleName.ToLower().Contains(searchString)
                                       || s.Category.CategoryName.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    sorter = sorter.OrderByDescending(s => s.ArticleName);
                    break;
                case "Author":
                    sorter = sorter.OrderBy(s => s.ArticleAuthor);
                    break;
                case "author_desc":
                    sorter = sorter.OrderByDescending(s => s.ArticleAuthor);
                    break;
                case "Category":
                    sorter = sorter.OrderBy(s => s.Category.CategoryName);
                    break;
                case "category_desc":
                    sorter = sorter.OrderByDescending(s => s.Category.CategoryName);
                    break;
                case "Date":
                    sorter = sorter.OrderBy(s => s.ArticleCreated);
                    break;
                case "date_desc":
                    sorter = sorter.OrderByDescending(s => s.ArticleCreated);
                    break;
                default:
                    sorter = sorter.OrderBy(s => s.ArticleName);
                    break;
            }

            int pageSize = 10;
            Articles = PaginatedList<Data.Article>.Create(
                sorter.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}