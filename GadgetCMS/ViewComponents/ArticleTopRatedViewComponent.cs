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
        public IQueryable<int> articleIds;
        public List<int> articleIdsParent = new List<int>();
        public List<double> ratingsAverage = new List<double>();
        public List<Data.Article> Articles = new List<Data.Article>();
        public ArticleTopRatedViewComponent(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
           //Articles = _context.Article.Where(e => e.ArticleVisible == true).Include(a => a.Reviews).Where(a => DateTime.Compare(a.ArticleCreated, DateTime.Today.AddMonths(-4)) >= 0).Take(5).ToList(); 
           //articleIds = _context.Review.Select(g => g.ArticleId).Distinct();
           // foreach(var articleIdsFE in articleIds)
           // {
           //     ratingsAverage.Add(_context.Review.Where(c => c.ArticleId == articleIdsFE).Average(r => r.ReviewRating));
           // }
            return View("Default",_context);
        }
    }
}
