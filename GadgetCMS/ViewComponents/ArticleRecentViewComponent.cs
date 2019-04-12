using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.ViewComponents
{
    public class ArticleRecentViewComponent : ViewComponent
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        public List<Data.Article> articlesRecent = new List<Data.Article>();
        public ArticleRecentViewComponent(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            articlesRecent = _context.Article.OrderByDescending(s => s.ArticleCreated).ToList();
            return View("Default",articlesRecent);
        }
    }
}
