using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ArticlePicture
{
    public class EditModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public EditModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.ArticlePicture ArticlePicture { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArticlePicture = await _context.ArticlePicture
                .Include(a => a.Article).FirstOrDefaultAsync(m => m.ArticlePictureId == id);

            if (ArticlePicture == null)
            {
                return NotFound();
            }
           ViewData["ArticleId"] = new SelectList(_context.Article, "ArticleId", "ArticleAuthor");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ArticlePicture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticlePictureExists(ArticlePicture.ArticlePictureId))
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

        private bool ArticlePictureExists(int id)
        {
            return _context.ArticlePicture.Any(e => e.ArticlePictureId == id);
        }
    }
}
