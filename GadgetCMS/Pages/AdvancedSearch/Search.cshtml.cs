using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GadgetCMS.Pages.AdvancedSearch
{
    public class SearchModel : PageModel
    {
        
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        public string category_selection;
        public int? category_selection_int;
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

        public PartialViewResult OnGetSelectCategory(string categorySelection)
        {
            if(categorySelection != null)
            {
                category_selection_int = Int32.Parse(categorySelection);   
                
            }
            if(category_selection_int != null)
            {
                articles_list = _context.Article.Where(b => b.CategoryId == category_selection_int).ToList();
            }


            return new PartialViewResult
            {
                ViewName = "_ArticleList",
                ViewData = new ViewDataDictionary<List<Data.Article>>(ViewData, articles_list)
            };
        }

         public PartialViewResult OnGetFilterArticles(string categorySelection)
        {
            if(categorySelection != null)
            {
                category_selection_int = Int32.Parse(categorySelection);
            }
            
            if(category_selection_int != null)
            {
                articles_list =  _context.Article.Where(b => b.CategoryId == category_selection_int).ToList();
                articles_listInt = articles_list.Select(z => z.ArticleId).ToList();
           
                fetchParentParameter = _context.CategoryParentParameter.Where(c => c.CategoryId == category_selection_int)
                    .Select(d => d.ParentParameterId).First();

                parameters_list = _context.Parameter.Where(d => d.ParentParameterId == fetchParentParameter).ToList();
                articleParameters_list = _context.ArticleParameter.ToList();

                 AdvancedSearch = new Data.AdvancedSearch
                {
                Articles = articles_list,
                Parameters = parameters_list,
                ArticleParameters = articleParameters_list
                };
            }
            else
            {
                AdvancedSearch = new Data.AdvancedSearch();
            }
            
            return new PartialViewResult
                {
                    ViewName = "_FiltersList",
                    ViewData = new ViewDataDictionary<Data.AdvancedSearch>(ViewData, AdvancedSearch)
                };
            
        }

        public PartialViewResult OnGetFilterArticlesFinal(string categorySelection,string values)
        {
            if(categorySelection != null && values != null)
            {
                category_selection_int = Int32.Parse(categorySelection);
                if(category_selection_int != null)
                {
                     articles_list =  _context.Article.Where(b => b.CategoryId == category_selection_int).ToList();
                    articles_listInt = articles_list.Select(z => z.ArticleId).ToList();

                    //parametervalues = values.Split("!");
                    //for (int i = 0; i < parametervalues.Length - 1; i++)
                    //{
                    //    articleid_temp = _context.ArticleParameter
                    //        .Where(b => b.ArticleParameterValue == parametervalues[i]).ToList();
                    //    articleids_filtered.Add(articleid_temp);
                    //}

                    FilteredArticle = new Data.FilteredArticle(_context)
                    {
                        ParameterValues = values,
                        Articles = articles_list
                
                    };
                }
                
           
            }
            else
            {
                FilteredArticle = new Data.FilteredArticle();
                   
            }

            return new PartialViewResult
            {
                ViewName = "_ArticleListFilteredFinal",
                ViewData = new ViewDataDictionary<Data.FilteredArticle>(ViewData, FilteredArticle)
            };
        }
    }
}