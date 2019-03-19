using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Areas.Dashboard.Pages.ArticleLog
{
    public class EditModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public EditModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.ArticleLog ArticleLog { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArticleLog = await _context.ArticleLog
                .Include(a => a.Article)
                .Include(a => a.GadgetCmsUser).FirstOrDefaultAsync(m => m.UserId == id);

            if (ArticleLog == null)
            {
                return NotFound();
            }
           ViewData["ArticleId"] = new SelectList(_context.Article, "ArticleId", "ArticleAuthor");
           ViewData["UserId"] = new SelectList(_context.Set<GadgetCMSUser>(), "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ArticleLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleLogExists(ArticleLog.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ArticleLogExists(string id)
        {
            return _context.ArticleLog.Any(e => e.UserId == id);
        }
    }
}
