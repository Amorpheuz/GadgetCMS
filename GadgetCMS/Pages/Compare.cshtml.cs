using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GadgetCMS.Pages
{
    public class CompareModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        //ArticleList (Based on CategorySelection)
        public List<Data.Article> ArticleNames = new List<Data.Article>();

        public CompareModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context =  context;
        }

        public Data.Article Article {get;set;}
        public void OnGet()
        {
             ViewData["CategoryId"] = new SelectList(_context.Category,"CategoryId","CategoryName");
        }

        public IActionResult OnGetSelectCategory(string category)
        {
            int? categoryInt = 0;
            if(category != null)
            {
                categoryInt = Int32.Parse(category);
                ArticleNames = _context.Article.Where(c => c.CategoryId == categoryInt).ToList();
            }
            
            List<Data.CategoryParentParameter> catParentParameters = _context.CategoryParentParameter.Where(c => c.CategoryId == categoryInt).ToList();
            List<Data.ParentParameter> parentParameters = new List<Data.ParentParameter>();
            List<List<Data.ParentParameter>> parentParametersMain = new List<List<Data.ParentParameter>>();
            List<Data.Parameter> parametersArr;
            List<List<Data.Parameter>> parametersMain = new List<List<Data.Parameter>>();
            foreach(var item in catParentParameters)
            {
                parametersArr = _context.Parameter.Where(c => c.ParentParameterId == item.ParentParameterId).ToList();
                parametersMain.Add(parametersArr);
            }

            MultipleList multipleList = new MultipleList
            {
                Articles = ArticleNames,
                CategoryParentParameters = catParentParameters,
                Parameters = parametersMain
                
            };

            return new JsonResult(multipleList);
        }

        public IActionResult OnGetBindArticle(string name)
        {
            int? articleId = 0;
            if(name != null)
            {
                 articleId = _context.Article.Where(c => c.ArticleName == name).Select(d => d.ArticleId).FirstOrDefault();
            }
           
            List<Data.ArticleParameter> articleParameters = _context.ArticleParameter.Where(c => c.ArticleId == articleId).ToList();
            
            var base64 = Convert.ToBase64String(_context.ArticlePicture.Where(c => c.ArticleId == articleId).First().ArticlePictureBytes);
            var imgSrc = $"data:image/gif;base64,{base64}";

            ArticleValuesWithPhoto articleValuesWithPhoto = new ArticleValuesWithPhoto
            {
                ArticleParameters = articleParameters,
                ImgSrc = imgSrc
            };

            return new JsonResult(articleValuesWithPhoto);
        }
    }

    public class MultipleList{
        public List<Data.Article> Articles {get;set;}
        public List<Data.CategoryParentParameter> CategoryParentParameters {get;set;}
        public List<List<Data.Parameter>> Parameters {get;set;}
        
    }

    public class ArticleValuesWithPhoto
    {
        public List<Data.ArticleParameter> ArticleParameters {get;set;}
        public string ImgSrc {get;set;}
    }
}