using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;
using GadgetCMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using GadgetCMS.Areas.Identity.Data;

namespace GadgetCMS.Pages.Review
{
    public class DeleteModel : PageModel
    {
        private readonly GadgetCMSContext _context;
        private readonly UserManager<GadgetCMSUser> _userManager;

        public DeleteModel(GadgetCMSContext context, UserManager<GadgetCMSUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Data.Review Review { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string userEmail)
        {
            if (userEmail == null)
            {
                return NotFound();
            }

            Review = await _context.Review
                .Include(r => r.Article)
                .Include(r => r.GadgetCmsUser).FirstOrDefaultAsync(m => m.ArticleId == id && m.GadgetCmsUser.Email == userEmail);

            var user = await _userManager.GetUserAsync(User);

            if (user.Id != Review.UserId)
            {
                return RedirectToPage("./Details", new { id, userEmail });
            }

            if (Review == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string userId = Review.UserId;
            int articleId = Review.ArticleId;

            if (userId == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user.Id == userId)
            {
                Review = await _context.Review.FindAsync(userId, articleId);

                if (Review != null)
                {
                    _context.Review.Remove(Review);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("/Article/Details", new { id = articleId });
            }
            return NotFound();
        }
    }
}
