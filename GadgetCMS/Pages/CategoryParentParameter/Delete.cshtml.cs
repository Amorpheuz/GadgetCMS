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
    public class DeleteModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public DeleteModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.CategoryParentParameter CategoryParentParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? ppId)
        {
            if (id == null)
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

        public async Task<IActionResult> OnPostAsync()
        {
            int categoryId = CategoryParentParameter.CategoryId;
            int parentParameterId = CategoryParentParameter.ParentParameterId;
            
            CategoryParentParameter = await _context.CategoryParentParameter.FindAsync(categoryId,parentParameterId);

            if (CategoryParentParameter != null)
            {
                _context.CategoryParentParameter.Remove(CategoryParentParameter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
