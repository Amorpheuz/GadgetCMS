using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ParentParameter
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
            return Page();
        }

        [BindProperty]
        public Data.ParentParameter ParentParameter { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParentParameter.Add(ParentParameter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}