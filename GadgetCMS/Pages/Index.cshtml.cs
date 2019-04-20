using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GadgetCMS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public IndexModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {

        }

        public List<Data.Article> Articles {get;set;} 

        public PartialViewResult OnGetSearchQuery(string value)
        {
            
            Articles = _context.Article.Where(s => s.ArticleContent.Contains(value)).OrderByDescending(c => c.ArticleCreated).Take(5).ToList();
           
            return new PartialViewResult
            {
                ViewName = "_ArticleListSearchQuery",
                ViewData = new ViewDataDictionary<List<Data.Article>>(ViewData, Articles)
            };
        }
    }
}
