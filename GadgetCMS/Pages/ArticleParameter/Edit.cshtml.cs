using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ArticleParameter
{
    public class EditModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public EditModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.ArticleParameter ArticleParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? pId)
        {
            if (id == null || pId == null)
            {
                return NotFound();
            }

            ArticleParameter = await _context.ArticleParameter
                .Include(a => a.Article)
                .Include(a => a.Parameter).FirstOrDefaultAsync(m => m.ArticleId == id && m.ParameterId == pId);

            if (ArticleParameter == null)
            {
                return NotFound();
            }
           ViewData["ArticleId"] = new SelectList(_context.Article, "ArticleId", "ArticleName");
           ViewData["ParameterId"] = new SelectList(_context.Parameter, "ParameterId", "ParameterName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ArticleParameter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleParameterExists(ArticleParameter.ArticleId,ArticleParameter.ParameterId))
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

        private bool ArticleParameterExists(int id, int pId)
        {
            return _context.ArticleParameter.Any(e => e.ArticleId == id && e.ParameterId == pId);
        }
    }
}
