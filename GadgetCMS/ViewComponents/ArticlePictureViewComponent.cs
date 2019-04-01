using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GadgetCMS.ViewComponents
{
    public class ArticlePictureViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ArticlePictureViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ArticlePicture> ArticlePictures { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(int articleId)
        {
            ArticlePictures = await _context.ArticlePicture
                .Include(a => a.Article).Where(a => a.ArticleId == articleId).ToListAsync();

            return View(ArticlePictures);
        }
    }
}
