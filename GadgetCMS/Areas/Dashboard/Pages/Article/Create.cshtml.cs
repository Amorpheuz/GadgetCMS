using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;
using NLog;
using Microsoft.AspNetCore.Identity;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace GadgetCMS.Pages.Article
{
    [Authorize(Roles = "Admin,Moderator,Editor")]
    public class CreateModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<GadgetCMSUser> _userManager;
        public CreateModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Data.Category>(), "CategoryId", "CategoryName");
            parameters = FindFirstParams();
            return Page();
        }

        [BindProperty]
        public Data.Article Article { get; set; }

        [BindProperty]
        public Data.ArticlePicture ArticlePictures { get; set; }

        public List<List<Data.Parameter>> parameters { get; set; }

        public async Task<IActionResult> OnPostAsync(List<IFormFile> upFiles, ICollection<string> vals, ICollection<int> valIds)
        {
            if (upFiles == null || valIds == null || vals == null)
            {
                ViewData["CategoryId"] = new SelectList(_context.Set<Data.Category>(), "CategoryId", "CategoryName");
                parameters = FindFirstParams();
                CheckArticleContent();
                ViewData["ImageCheck"] = false;
                return Page();
            }
            else if(!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Set<Data.Category>(), "CategoryId", "CategoryName");
                CheckArticleContent();
                parameters = FindFirstParams();
                return Page();
            }
            if (vals.Contains(null))
            {
                ViewData["CategoryId"] = new SelectList(_context.Set<Data.Category>(), "CategoryId", "CategoryName");
                CheckArticleContent();
                parameters = FindFirstParams();
                ViewData["ValError"] = "Please fill all the parameter inputs";
                return Page();
            }
            string[] captionList = ArticlePictures.ArticlePictureCaption.Split(";");
            if (upFiles.Count != captionList.Count())
            {
                ViewData["CategoryId"] = new SelectList(_context.Set<Data.Category>(), "CategoryId", "CategoryName");
                parameters = FindFirstParams();
                CheckArticleContent();
                ViewData["CaptionError"] = "Please enter caption for each image seperated by `;`";
                return Page();
            }
            foreach (var item in captionList)
            {
                if (item == null || item == "" || item==" ")
                {
                    ViewData["CategoryId"] = new SelectList(_context.Set<Data.Category>(), "CategoryId", "CategoryName");
                    ViewData["CaptionError"] = "Empty or Missing Caption Detected, Please enter caption for each image seperated by `;`";
                    CheckArticleContent();
                    parameters = FindFirstParams();
                    return Page();
                }
            }
            _context.Article.Add(Article);
            await _context.SaveChangesAsync();
            var user2 = await _userManager.GetUserAsync(User);
            logger.Info("{user} created article {article} - carrying id {id} on {date}",user2.Email,Article.ArticleName,Article.ArticleId,DateTime.Now);

            var articleId = Article.ArticleId;
            var counter = 0;
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
                        ArticlePictureCaption = captionList[counter++],
                        ArticlePictureBytes = ms.ToArray()
                    };
                    _context.ArticlePicture.Add(upArticlePicture);
                }
            }
            await _context.SaveChangesAsync();

            for (int i = 0; i < vals.Count; i++)
            {
                Data.ArticleParameter articleParameter = new Data.ArticleParameter()
                {
                    ArticleId = articleId,
                    ParameterId = valIds.ElementAt(i),
                    ParameterVal = vals.ElementAt(i)
                };

                _context.ArticleParameter.Add(articleParameter);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("../Articles");
        }

        public IActionResult OnGetFetchParameter(int id)
        {
            List<Data.CategoryParentParameter> temp = _context.CategoryParentParameter
                .Include(r => r.ParentParameter)
                .ThenInclude(r => r.Parameters)
                .Where(r => r.CategoryId == id).ToList();
            
            parameters = temp.Select(r => r.ParentParameter.Parameters).ToList();

            JsonResult jsonResult = new JsonResult(parameters);
            return jsonResult;
        }

        private List<List<Data.Parameter>> FindFirstParams()
        {
            int id = _context.Category.Select(r => r.CategoryId).FirstOrDefault();
            List<Data.CategoryParentParameter> temp = _context.CategoryParentParameter
                .Include(r => r.ParentParameter)
                .ThenInclude(r => r.Parameters)
                .Where(r => r.CategoryId == id).ToList();
            var tempParam = temp.Select(r => r.ParentParameter.Parameters).ToList();
            return tempParam;
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