using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Http;

namespace GadgetCMS.Pages.ArticlePicture
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
        ViewData["ArticleId"] = new SelectList(_context.Article, "ArticleId", "ArticleName");
            return Page();
        }

        [BindProperty]
        public Data.ArticlePicture ArticlePicture { get; set; }

        public async Task<IActionResult> OnPostAsync(List<IFormFile> upFiles)
        {
            foreach (var upFile in upFiles)
            {
                if (upFile != null || upFile.ContentType.ToLower().StartsWith("image/"))
                {
                    MemoryStream ms = new MemoryStream();
                    upFile.OpenReadStream().CopyTo(ms);

                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                    Data.ArticlePicture upArticlePicture= new Data.ArticlePicture()
                    {
                        ArticleId = ArticlePicture.ArticleId,
                        ArticlePictureCaption = ArticlePicture.ArticlePictureCaption,
                        ArticlePictureBytes = ms.ToArray()
                    };

                    _context.ArticlePicture.Add(upArticlePicture);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}