using GadgetCMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.ViewComponents
{
    public class CategoryNamesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CategoryNamesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public CategoryDetails CategoryDetails { get; set; }

        public IViewComponentResult Invoke(string companyName)
        {
            if(companyName == "0")
            {
                CategoryDetails = new CategoryDetails
                {
                    CategoryNames = _context.Category.Select(c => c.CategoryName).ToList(),
                    CategoryIds = _context.Category.Select(c => c.CategoryId).ToList()
                };
            }
            else
            {
                CategoryDetails = new CategoryDetails
                {
                    CategoryNames = _context.ArticleParameter
                        .Include(c => c.Article)
                            .ThenInclude(c => c.Category)
                        .Where(c => c.ParameterVal == companyName && c.ParameterId == 5)
                        .Select(c => c.Article.Category.CategoryName).ToList(),
                    CategoryIds = _context.ArticleParameter
                        .Include(c => c.Article)
                            .ThenInclude(c => c.Category)
                        .Where(c => c.ParameterVal == companyName && c.ParameterId == 5)
                        .Select(c => c.Article.Category.CategoryId).ToList()
                };
            }
            return View(CategoryDetails);
        }
    }

    public class CategoryDetails
    {
        public List<string> CategoryNames { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
