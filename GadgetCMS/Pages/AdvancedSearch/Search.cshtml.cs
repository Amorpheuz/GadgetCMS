using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GadgetCMS.Pages.AdvancedSearch
{
    public class SearchModel : PageModel
    {
        
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        public string category_selection;
        public int category_selection_int;
        public List<Data.Article> articles_list = new List<Data.Article>();
        
        public List<Data.Parameter> parameters_list = new List<Data.Parameter>();
        public List<Data.ArticleParameter> articleParameters_list = new List<Data.ArticleParameter>();
       
        public List<List<Data.ArticleParameter>> articleids_filtered = new List<List<Data.ArticleParameter>>();

        
        public List<int> FilteredArticleListFinal = new List<int>();


        public List<Data.ArticleParameter> articleid_temp = new List<Data.ArticleParameter>();
        public List<string> articleparameters_param = new List<string>();
        public static int flag = 0;
        public int checkboxcountA = 1;
        public int fetchParentParameter;
        public string[] parametervalues;
        public List<int> articles_listInt = new List<int>();

        public SearchModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.Article Article{get;set;}
        public Data.CategoryParentParameter CategoryParentParameter {get;set;}
        public Data.Parameter Parameter {get;set;}
        public Data.CategoryParentParameter categoryParentParameter {get;set;}
        public Data.ArticleParameter ArticleParameter {get; set; }

        public Data.AdvancedSearch AdvancedSearch;
        public Data.FilteredArticle FilteredArticle;
        public void OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            articles_list = null;
        }

    }
}