using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Areas.Dashboard.Pages.ArticleLog
{
    public class DeleteModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public DeleteModel(GadgetCMS.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArticleLog = await _context.ArticleLog.FindAsync(id);

            if (ArticleLog != null)
            {
                _context.ArticleLog.Remove(ArticleLog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
