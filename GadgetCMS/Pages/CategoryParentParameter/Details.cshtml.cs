using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.CategoryParentParameter
{
    public class DetailsModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public DetailsModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.CategoryParentParameter CategoryParentParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? ppId)
        {
            if (id == null || ppId == null)
            {
                return NotFound();
            }

            CategoryParentParameter = await _context.CategoryParentParameter
                .Include(c => c.Category)
                .Include(c => c.ParentParameter).FirstOrDefaultAsync(m => m.CategoryId == id && m.ParentParameterId == ppId);

            if (CategoryParentParameter == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
