using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.CategoryParentParameter
{
    public class CreateModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public CreateModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
        ViewData["ParentParameterId"] = new SelectList(_context.ParentParameter, "ParentParameterId", "ParentParameterName");
            return Page();
        }

        [BindProperty]
        public Data.CategoryParentParameter CategoryParentParameter { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CategoryParentParameter.Add(CategoryParentParameter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}