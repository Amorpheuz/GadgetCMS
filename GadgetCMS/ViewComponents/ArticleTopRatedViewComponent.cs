using Microsoft.AspNetCore.Mvc;
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
        public ArticleTopRatedViewComponent(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            
           articleIds = _context.Review.Select(g => g.ArticleId).Distinct();
            foreach(var articleIdsFE in articleIds)
            {
                ratingsAverage.Add(_context.Review.Where(c => c.ArticleId == articleIdsFE).Average(r => r.ReviewRating));
            }
            return View("Default",_context);
        }
    }
}
