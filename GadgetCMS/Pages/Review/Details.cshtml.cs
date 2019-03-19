using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;
using GadgetCMS.Models;

namespace GadgetCMS.Pages.Review
{
    public class DetailsModel : PageModel
    {
        private readonly GadgetCMSContext _context;

        public DetailsModel(GadgetCMSContext context)
        {
            _context = context;
        }

        public Data.Review Review { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, string userEmail)
        {
            if (userEmail == null || id == null)
            {
                return NotFound();
            }

            Review = await _context.Review
                .Include(r => r.Article)
                .Include(r => r.GadgetCmsUser).FirstOrDefaultAsync(m => m.ArticleId == id && m.GadgetCmsUser.Email == userEmail);

            if (Review == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
