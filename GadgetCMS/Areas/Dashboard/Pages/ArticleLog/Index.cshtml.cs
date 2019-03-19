﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Areas.Dashboard.Pages.ArticleLog
{
    public class IndexModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public IndexModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.ArticleLog> ArticleLog { get;set; }

        public async Task OnGetAsync()
        {
            ArticleLog = await _context.ArticleLog
                .Include(a => a.Article)
                .Include(a => a.GadgetCmsUser).ToListAsync();
        }
    }
}
