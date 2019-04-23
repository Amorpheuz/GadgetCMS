using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GadgetCMS.Areas.Identity.Data;
using GadgetCMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GadgetCMS.ViewComponents
{
    public class ReviewViewComponent : ViewComponent
    {
        private readonly GadgetCMSContext _context;
        private readonly UserManager<GadgetCMSUser> _userManager;
        public ReviewViewComponent(UserManager<GadgetCMSUser> userManager, GadgetCMSContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Data.Review> Reviews { get; set; }
                
        public async Task<IViewComponentResult> InvokeAsync(int ArticleId)
        {
            Reviews = await _context.Review
                .Include(r => r.Article)
                .Include(r => r.GadgetCmsUser).Where(r => r.ArticleId == ArticleId).ToListAsync();

            var user = await _userManager.GetUserAsync(HttpContext.User);
            foreach (var item in Reviews)
            {
                if(user != null)
                {
                    if (item.GadgetCmsUser.Email == user.Email)
                    {
                        ViewData["UserHasComment"] = true;
                    }
                }
            }
            ViewData["ArtId"] = ArticleId;
            return View(Reviews);
        }
    }
}
