using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;
using NLog;
using Microsoft.AspNetCore.Identity;
using GadgetCMS.Areas.Identity.Data;

namespace GadgetCMS.Pages.Category
{
    public class DeleteModel : PageModel
    {
        public GadgetCMS.Data.ApplicationDbContext _context;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<GadgetCMSUser> _userManager;
        public DeleteModel(GadgetCMS.Data.ApplicationDbContext context,UserManager<GadgetCMSUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Data.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.Include(c => c.CategoryParentParameters).FirstOrDefaultAsync(m => m.CategoryId == id);

            if (Category == null)
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

            Category = await _context.Category.FindAsync(id);

            if (Category != null)
            {
                _context.Category.Remove(Category);
                await _context.SaveChangesAsync();
            }
            var user = await _userManager.GetUserAsync(User);
            logger.Info("{user} deteled category {category} - carrying {id} on {date}",user.Email,Category.CategoryName,Category.CategoryId,DateTime.Now);
            return RedirectToPage("../Categories");
        }
    }
}
