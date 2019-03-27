using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GadgetCMS.ViewComponents
{
    public class ReviewViewComponent : ViewComponent
    {
        private readonly GadgetCMSContext _context;
        public ReviewViewComponent(GadgetCMSContext context)
        {
            _context = context;
        }

        public List<Data.Review> Reviews { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(int ArticleId)
        {
            Reviews = await _context.Review
                .Include(r => r.Article)
                .Include(r => r.GadgetCmsUser).Where(r => r.ArticleId == ArticleId).ToListAsync();
            
            return View(Reviews);
        }
    }
}
