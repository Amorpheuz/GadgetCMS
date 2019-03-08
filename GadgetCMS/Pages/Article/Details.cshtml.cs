using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.Article
{
    public class DetailsModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public DetailsModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Article.FirstOrDefaultAsync(m => m.ArticleId == id);

            if (Article == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
