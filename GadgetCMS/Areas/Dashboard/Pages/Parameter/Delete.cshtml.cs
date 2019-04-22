using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ParentParameter
{
    public class DeleteModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public DeleteModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.ParentParameter ParentParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParentParameter = await _context.ParentParameter.Include(m => m.Parameters).FirstOrDefaultAsync(m => m.ParentParameterId == id);

            if (ParentParameter == null)
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

            ParentParameter = await _context.ParentParameter.FindAsync(id);

            if (ParentParameter != null)
            {
                _context.ParentParameter.Remove(ParentParameter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Parameters");
        }
    }
}
