using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.Review
{
    public class DetailsModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public DetailsModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.Review Review { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review = await _context.Review
                .Include(r => r.Article)
                .Include(r => r.GadgetCmsUser).FirstOrDefaultAsync(m => m.UserId == id);

            if (Review == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
