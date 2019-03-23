using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ParentParameter
{
    public class EditModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public EditModel(GadgetCMS.Data.ApplicationDbContext context)
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

            ParentParameter = await _context.ParentParameter.FirstOrDefaultAsync(m => m.ParentParameterId == id);

            if (ParentParameter == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ParentParameter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentParameterExists(ParentParameter.ParentParameterId))
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

        private bool ParentParameterExists(int id)
        {
            return _context.ParentParameter.Any(e => e.ParentParameterId == id);
        }
    }
}
