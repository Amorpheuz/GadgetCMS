using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ArticlePicture
{
    public class IndexModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public IndexModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.ArticlePicture> ArticlePicture { get;set; }

        public async Task OnGetAsync()
        {
            ArticlePicture = await _context.ArticlePicture
                .Include(a => a.Article).ToListAsync();
        }
    }
}
