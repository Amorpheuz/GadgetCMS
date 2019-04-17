using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Identity;

namespace GadgetCMS.Pages.Review
{
    public class CreateModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        private readonly UserManager<GadgetCMSUser> _userManager;

        public CreateModel(GadgetCMS.Data.ApplicationDbContext context, UserManager<GadgetCMSUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet(int articleId)
        {
            ViewData["ArticleId"] = _context.Article.FirstOrDefault(a => a.ArticleId == articleId).ArticleId;
            ViewData["ArticleName"] = _context.Article.FirstOrDefault(a => a.ArticleId == articleId).ArticleName;
            ViewData["UserId"] = _userManager.GetUserId(User);
            return Page();
        }

        [BindProperty]
        public Data.Review Review { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if(Review.ReviewContent != null)
                {
                    ViewData["ReviewContent"] = Review.ReviewContent;
                }
                ViewData["ArticleId"] = _context.Article.FirstOrDefault(a => a.ArticleId == Review.ArticleId).ArticleId;
                ViewData["ArticleName"] = _context.Article.FirstOrDefault(a => a.ArticleId == Review.ArticleId).ArticleName;
                ViewData["UserId"] = _userManager.GetUserId(User);
                return Page();
            }

            _context.Review.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Article/Details",new { id = Review.ArticleId});
        }
    }
}