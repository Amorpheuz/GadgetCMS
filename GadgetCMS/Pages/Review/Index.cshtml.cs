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
    public class IndexModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        private readonly GadgetCMSContext _userContext;

        public IndexModel(GadgetCMS.Data.ApplicationDbContext context, GadgetCMSContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public IList<Data.Review> Review { get;set; }
        public List<string> Users { get; set; }

        public async Task OnGetAsync()
        {
            Review = await _userContext.Review
                .Include(r => r.Article).Include(r => r.GadgetCmsUser).ToListAsync();
        }
    }
}
