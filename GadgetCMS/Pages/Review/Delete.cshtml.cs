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
using NLog;

namespace GadgetCMS.Pages.Review
{
    public class DeleteModel : PageModel
    {
        private readonly GadgetCMSContext _context;
        private readonly UserManager<GadgetCMSUser> _userManager;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        
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
                    logger.Info("{user} removed review on article carrying - id {id} on {date}",user.Email,articleId,DateTime.Now);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("/Article/Details", new { id = articleId });
            }
            return NotFound();
        }

        public async Task<IActionResult> OnGetModDeleteAsync(int? id, string userEmail, string actionType)
        {
            if(userEmail == null || id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(userEmail);
            var userId = user.Id;

            var userIsAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            var userIsMod = await _userManager.IsInRoleAsync(user, "Moderator");

            if (userIsAdmin || userIsMod)
            {
                Review = await _context.Review.FindAsync(userId, id);

                if (Review != null)
                {
                    if (actionType == "delete")
                    {
                        Review.ReviewVisible = false;
                    }
                    else if(actionType == "undo")
                    {
                        Review.ReviewVisible = true;
                    }
                    _context.Attach(Review).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("/Reviews", new { area = "Dashboard"});
            }
            return NotFound();

        }
    }
}
