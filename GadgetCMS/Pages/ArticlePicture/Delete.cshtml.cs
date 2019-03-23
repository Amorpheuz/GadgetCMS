using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ArticlePicture
{
    public class DeleteModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public DeleteModel(GadgetCMS.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArticlePicture = await _context.ArticlePicture.FindAsync(id);

            if (ArticlePicture != null)
            {
                _context.ArticlePicture.Remove(ArticlePicture);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
