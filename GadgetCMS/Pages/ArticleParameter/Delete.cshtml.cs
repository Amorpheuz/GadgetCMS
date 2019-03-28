using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ArticleParameter
{
    public class DeleteModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public DeleteModel(GadgetCMS.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var articleId = ArticleParameter.ArticleId;
            var parameterId = ArticleParameter.ParameterId;

            ArticleParameter = await _context.ArticleParameter.FindAsync(articleId,parameterId);

            if (ArticleParameter != null)
            {
                _context.ArticleParameter.Remove(ArticleParameter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
