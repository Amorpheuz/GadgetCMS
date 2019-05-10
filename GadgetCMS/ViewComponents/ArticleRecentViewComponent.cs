using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.ViewComponents
{
    public class ArticleRecentViewComponent : ViewComponent
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        public List<Data.Article> articlesRecent { get;set;}
        public ArticleRecentViewComponent(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IViewComponentResult Invoke()
        {
            articlesRecent = _context.Article.Where(e => e.ArticleVisible == true).Include(c => c.ArticlePictures).OrderByDescending(s => s.ArticleCreated).ToList();
            return View("Default",articlesRecent);
        }
    }
}
