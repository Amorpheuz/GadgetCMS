using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.Article
{
    public class IndexModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public IndexModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Article> Article { get;set; }

        public async Task OnGetAsync()
        {
            Article = await _context.Article
                .Include(a => a.Category).ToListAsync();
        }

        public async Task OnGetCategoryAsync(int categoryId)
        {
            Article = await _context.Article
                .Include(a => a.Category)
                .Where(a => a.CategoryId == categoryId).ToListAsync();

            ViewData["ViewType"] = "Category";
        }

        public async Task OnGetCompanyAsync(string parameterVal)
        {
            var temp = await _context.ArticleParameter
                .Include(a => a.Article)
                    .ThenInclude(a => a.Category)
                .Where(a => a.ParameterVal == parameterVal && a.ParameterId == 5).ToListAsync();

            Article = temp.Select(a => a.Article).ToList();
            ViewData["ViewType"] = "Company";
        }
    }
}
