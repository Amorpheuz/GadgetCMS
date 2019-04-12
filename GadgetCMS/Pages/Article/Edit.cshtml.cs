using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.Article
{
    public class EditModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public EditModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Article Article { get; set; }

        [BindProperty]
        public List<Data.ArticleParameter> ArticleParameters { get; set; }

        public List<IGrouping<int, Data.ArticleParameter>> ListParameters { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Article
                .Include(a => a.Category)
                .Include(a => a.ArticleParameters)
                .Include(a => a.ArticlePictures)
                .FirstOrDefaultAsync(m => m.ArticleId == id);

            ListParameters = await _context.ArticleParameter
                .Include(a => a.Article)
                .Include(a => a.Parameter)
                    .ThenInclude(a => a.ParentParameter)
                .Where(a => a.ArticleId == id).GroupBy(a => a.Parameter.ParentParameter.ParentParameterId).ToListAsync();

            ArticleParameters = ListParameters.SelectMany(a => a).ToList();
            
            if (Article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Data.Category>(), "CategoryId", "CategoryName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Set<Data.Category>(), "CategoryId", "CategoryName");
                CheckArticleContent();
                return Page();
            }

            _context.Update(Article);
            _context.UpdateRange(ArticleParameters);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.ArticleId))
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

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.ArticleId == id);
        }

        private void CheckArticleContent()
        {
            if (Article.ArticleContent != null || Article.ArticleContent != "")
            {
                ViewData["ArticleContent"] = Article.ArticleContent;
            }
        }
    }
}
