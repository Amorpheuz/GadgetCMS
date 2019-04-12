using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

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

        private static int ArticleId { get; set; }

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

            ArticleId = Article.ArticleId;
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

        public IActionResult OnPostDeletePic(int PictureId)
        {
            Data.ArticlePicture articlePicture = _context.ArticlePicture.Find(PictureId);
            var status = "fail";
            if (articlePicture != null)
            {
                _context.ArticlePicture.Remove(articlePicture);
                _context.SaveChanges();
                status = "#image" + PictureId;
                return new JsonResult(status);
            }
            return new JsonResult(status);
        }
        
        public IActionResult OnPostAddPic(List<IFormFile> UploadImages)
        {
            var artId = ArticleId;
            foreach (var upFile in UploadImages)
            {
                if (upFile != null || upFile.ContentType.ToLower().StartsWith("image/"))
                {
                    MemoryStream ms = new MemoryStream();
                    upFile.OpenReadStream().CopyTo(ms);

                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                    

                    Data.ArticlePicture upArticlePicture = new Data.ArticlePicture()
                    {
                        ArticleId = artId,
                        ArticlePictureCaption = "-",
                        ArticlePictureBytes = ms.ToArray()
                    };
                    _context.ArticlePicture.Add(upArticlePicture);
                }
            }
            _context.SaveChanges();

            List<Data.ArticlePicture> articlePictures = _context.ArticlePicture.Where(a => a.ArticleId == artId).ToList();
            return new JsonResult(articlePictures);
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
