using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GadgetCMS.Data;

namespace GadgetCMS.Areas.Dashboard.Pages.ArticleLog
{
    public class CreateModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public CreateModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ArticleId"] = new SelectList(_context.Article, "ArticleId", "ArticleAuthor");
        ViewData["UserId"] = new SelectList(_context.Set<GadgetCMSUser>(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Data.ArticleLog ArticleLog { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ArticleLog.Add(ArticleLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}