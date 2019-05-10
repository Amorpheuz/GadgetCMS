using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.ViewComponents
{
    public class ArticleTopRatedViewComponent : ViewComponent
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        public List<Data.Article> Articles = new List<Data.Article>();
        public ArticleTopRatedViewComponent(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
           Articles = _context.Article.Where(c => c.ArticleRating > 3.9).Include(c => c.ArticlePictures).OrderByDescending(c => c.ArticleRating).Take(5).ToList();
           return View("Default",Articles);
        }
    }
}
