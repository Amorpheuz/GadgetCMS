using GadgetCMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.ViewComponents
{
    public class CompanyNamesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CompanyNamesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<string> CompanyNames { get; set; }

        public IViewComponentResult Invoke(int categoryId)
        {
            if (categoryId == 0)
            {
                CompanyNames = _context.ArticleParameter
                    .Where(a => a.ParameterId == 5).Select(a => a.ParameterVal).Distinct().ToList();
            }
            else
            {
                CompanyNames = _context.ArticleParameter
                    .Include(a => a.Article)
                    .Where(a => a.ParameterId == 5 && a.Article.CategoryId == categoryId).Select(a => a.ParameterVal).Distinct().ToList();
            }
            return View(CompanyNames);
        }
    }
}
