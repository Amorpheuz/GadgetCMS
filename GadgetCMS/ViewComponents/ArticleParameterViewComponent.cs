using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GadgetCMS.ViewComponents
{
    public class ArticleParameterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ArticleParameterViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ArticleParameter> ArticleParameters { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(int articleId)
        {
            ArticleParameters = await _context.ArticleParameter
                .Include(r => r.Parameter)
                .ThenInclude(r => r.ParentParameter).Where(r => r.ArticleId == articleId).ToListAsync();
            return View(ArticleParameters);
        }
    }
}
