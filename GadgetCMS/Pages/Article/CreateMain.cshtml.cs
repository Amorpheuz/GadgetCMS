﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace GadgetCMS.Pages.Article
{
    public class CreateMainModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public CreateMainModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Set<Data.Category>(), "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Data.Article Article { get; set; }

        [BindProperty]
        public Data.ArticlePicture ArticlePictures { get; set; }

        public async Task<IActionResult> OnPostAsync(List<IFormFile> upFiles)
        {
            _context.Article.Add(Article);
            await _context.SaveChangesAsync();

            var articleId = _context.Article.Select(a => a.ArticleId).Last();

            foreach (var upFile in upFiles)
            {
                if (upFile != null || upFile.ContentType.ToLower().StartsWith("image/"))
                {
                    MemoryStream ms = new MemoryStream();
                    upFile.OpenReadStream().CopyTo(ms);

                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                    Data.ArticlePicture upArticlePicture = new Data.ArticlePicture()
                    {
                        ArticleId = articleId,
                        ArticlePictureCaption = ArticlePictures.ArticlePictureCaption,
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