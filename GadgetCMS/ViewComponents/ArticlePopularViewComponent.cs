using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.ViewComponents
{
    public class ArticlePopularViewComponent : ViewComponent
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        public Data.Article articlesPopular = new Data.Article();
        public List<Data.Article> articlesPopularY =  new List<Data.Article>();
        public List<Data.Article> articlesPopularParent = new List<Data.Article>();
        public List<int> reviews = new List<int>();
        
        public ArticlePopularViewComponent(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            articlesPopularY = _context.Article.Where(c => c.ArticleVisible == true).Include(c => c.ArticlePictures).OrderByDescending(a => a.Reviews.Count()).Where(a => DateTime.Compare(a.ArticleCreated, DateTime.Today.AddMonths(-4)) >= 0).Take(5).ToList();

            return View("Default", articlesPopularY);
        }
    }
}
