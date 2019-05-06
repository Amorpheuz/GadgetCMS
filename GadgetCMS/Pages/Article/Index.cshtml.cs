using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using GadgetCMS.Areas.Dashboard.Pages;
namespace GadgetCMS.Pages.Article
{
    public class IndexModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;


        //----------------------------------YASH-------------------------------
        //public IndexModel(GadgetCMS.Data.ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public IList<Data.Article> Article { get;set; }

        //public async Task OnGetAsync()
        //{
        //    Article = await _context.Article
        //        .Include(a => a.Category)
        //        .Include(a => a.ArticlePictures)
        //        .OrderByDescending(a => a.ArticleCreated)
        //        .ToListAsync();
        //}

        //public async Task OnGetCategoryAsync(int categoryId)
        //{
        //    Article = await _context.Article
        //        .Include(a => a.Category)
        //        .Include(a => a.ArticlePictures)
        //        .Where(a => a.CategoryId == categoryId).ToListAsync();

        //    ViewData["ViewType"] = "Category";
        //}

        //public async Task OnGetCompanyAsync(string parameterVal)
        //{
        //    var temp = await _context.ArticleParameter
        //        .Include(a => a.Article)
        //            .ThenInclude(a => a.Category)
        //        .Include(a => a.Article)
        //            .ThenInclude(a => a.ArticlePictures)
        //        .Where(a => a.ParameterVal == parameterVal && a.ParameterId == 5).ToListAsync();

        //    Article = temp.Select(a => a.Article).ToList();
        //    ViewData["Company"] = parameterVal;
        //    ViewData["ViewType"] = "Company";
        //}




        //-------------------------ALKESH-------------------------------
        public string category_selection;
        public int category_selection_int;
        public List<Data.Article> articles_list = new List<Data.Article>();
        //public List<Data.Article> articles_list2;
        public List<Data.Parameter> parameters_list = new List<Data.Parameter>();
        public List<List<Data.Parameter>> paramters_listMain = new List<List<Data.Parameter>>();

        public List<Data.ArticleParameter> articleParameters_list = new List<Data.ArticleParameter>();
       
        public List<List<Data.ArticleParameter>> articleids_filtered = new List<List<Data.ArticleParameter>>();

        //Alkesh's Filtered Article List try
        public List<int> FilteredArticleListFinal = new List<int>();


        public List<Data.ArticleParameter> articleid_temp = new List<Data.ArticleParameter>();
        public List<string> articleparameters_param = new List<string>();
        public static int flag = 0;
        public int checkboxcountA = 1;
        public List<int> fetchParentParameter = new List<int>();
        public string[] parametervalues;
        public string[] categoryValues;
        public int? categoryIds = null;

        public List<int> articles_listInt = new List<int>();

        public List<List<Data.Article>> articlesBasedOnCategorySelection = new List<List<Data.Article>>();

        public List<Data.Category> categories = new List<Data.Category>();

        public IndexModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.Article Article{get;set;}
        public Data.CategoryParentParameter CategoryParentParameter {get;set;}
        public Data.Parameter Parameter {get;set;}
        public Data.CategoryParentParameter categoryParentParameter {get;set;}
        public Data.ArticleParameter ArticleParameter {get; set; }

        public AdvancedSearch AdvancedSearch;
        public FilteredArticle FilteredArticle;

        public PaginatedList<Data.Article> articlesIndex {get;set;}
        public void OnGet(int? pageIndex)
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            articles_list = null;
            IQueryable<Data.Article> studentIQ4 = from s in _context.Article.OrderByDescending(s => s.ArticleCreated).Include(c => c.ArticlePictures)
                                    select s;

            int pageSize = 10;
             articlesIndex = PaginatedList<Data.Article>.Create(
                studentIQ4.AsNoTracking(), pageIndex ?? 1, pageSize);
           
        }

        public PaginatedList<Data.Article> articlesRecent {get;set;}
        public IActionResult OnGetArticleRecent(int? pageIndex)
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            IQueryable<Data.Article> studentIQ2 = from s in _context.Article.OrderByDescending(s => s.ArticleCreated).Include(c => c.ArticlePictures)
                                    select s;

            int pageSize = 5;
             articlesRecent = PaginatedList<Data.Article>.Create(
                studentIQ2.AsNoTracking(), pageIndex ?? 1, pageSize);
           
            return Page();
        }

        
        public PaginatedList<Data.Article> articlesPopularParentPaginatedList {get;set;}
        public List<Data.Review> reviews = new List<Data.Review>();
        public IActionResult OnGetArticlePopular(int? pageIndex)
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            reviews = _context.Review.ToList();

            IQueryable<Data.Article> studentIQ3 = from s in _context.Article.Include(c => c.ArticlePictures).Include(d => d.Reviews)
                                    select s;
            int pageSize = 5;
             articlesPopularParentPaginatedList = PaginatedList<Data.Article>.Create(
                studentIQ3.AsNoTracking(), pageIndex ?? 1, pageSize);
            return Page();
        }


        public PartialViewResult OnGetSearchQuery(string value)
        {
            articles_list = _context.Article.Where(s => s.ArticleName.Contains(value)).OrderByDescending(c => c.ArticleCreated).Take(5).ToList();
           
            return new PartialViewResult
            {
                ViewName = "_ArticleListSearchQuery",
                ViewData = new ViewDataDictionary<List<Data.Article>>(ViewData, articles_list)
            };
        }

        public PartialViewResult OnGetSelectCategory(string categorySelection,string searchQuery)
        {
            //categoryValues = categorySelection.Split(";");

            //for(int i=0;i<categoryValues.Length - 1;i++)
            //{
            //    categoryIds = _context.Category.Where(c => c.CategoryName == categoryValues[i]).Select(d => d.CategoryId).Single();
            //    //category_selection_int = Int32.Parse(categoryValues[i]);   
            //    articles_list = _context.Article.Where(b => b.CategoryId == categoryIds).ToList();
            //    articlesBasedOnCategorySelection.Add(articles_list);
            //}

            category_selection_int = Int32.Parse(categorySelection);

            if(searchQuery != null)
            {
                articles_list = _context.Article.Where(b => b.CategoryId == category_selection_int).Where(c => c.ArticleName.Contains(searchQuery)).Include(c => c.ArticlePictures).ToList();
            }
            else
            {
                articles_list = _context.Article.Where(b => b.CategoryId == category_selection_int).Include(c => c.ArticlePictures).ToList();
            }
           
            return new PartialViewResult
            {
                ViewName = "_ArticleList",
                ViewData = new ViewDataDictionary<List<Data.Article>>(ViewData, articles_list)
            };
        }

        public PartialViewResult OnGetFilterArticles(string categorySelection)
        {
            category_selection_int = Int32.Parse(categorySelection);

            articles_list =  _context.Article.Where(b => b.CategoryId == category_selection_int).ToList();
            articles_listInt = articles_list.Select(z => z.ArticleId).ToList();
           
            fetchParentParameter = _context.CategoryParentParameter.Where(c => c.CategoryId == category_selection_int)
                .Select(d => d.ParentParameterId).ToList();

            foreach(var fetchParentParameterFE in fetchParentParameter)
            {
                parameters_list = _context.Parameter.Where(c => c.ParentParameterId == fetchParentParameterFE).ToList();
                paramters_listMain.Add(parameters_list);
            }

            articleParameters_list = _context.ArticleParameter.ToList();

            AdvancedSearch = new AdvancedSearch()
            {
                Articles = articles_list,
                Parameters = parameters_list,
                ParametersMain = paramters_listMain,
                ArticleParameters = articleParameters_list
            };
                
            return new PartialViewResult
                {
                    ViewName = "_FiltersList",
                    ViewData = new ViewDataDictionary<AdvancedSearch>(ViewData, AdvancedSearch)
                };
            
        }

        public PartialViewResult OnGetFilterArticlesFinal(string categorySelection,string values,string searchQuery)
        {

            category_selection_int = Int32.Parse(categorySelection);

            if(searchQuery != null)
            {
                articles_list = _context.Article.Where(b => b.CategoryId == category_selection_int).Where(c => c.ArticleName.Contains(searchQuery)).Include(c => c.ArticlePictures).ToList();
                articles_listInt = articles_list.Select(z => z.ArticleId).ToList();
            }
            else
            {
                articles_list = _context.Article.Where(b => b.CategoryId == category_selection_int).Include(c => c.ArticlePictures).ToList();
                articles_listInt = articles_list.Select(z => z.ArticleId).ToList();

            }

            FilteredArticle = new FilteredArticle(_context)
            {
                ParameterValues = values,
                Articles = articles_list
                
            };

            return new PartialViewResult
            {
                ViewName = "_ArticleListFilteredFinal",
                ViewData = new ViewDataDictionary<FilteredArticle>(ViewData, FilteredArticle)
            };
        }
    }
    public class AdvancedSearch
    {
         public List<Data.Article> Articles{get;set;}
        public List<Data.Parameter> Parameters {get;set;}
       
        public List<List<Data.Parameter>> ParametersMain {get;set;}
        public List<Data.ArticleParameter> ArticleParameters { get; set; }
    }

    public class FilteredArticle
    {
        public string ParameterValues {get;set;}
        public List<Data.Article> Articles {get;set;}
        private GadgetCMS.Data.ApplicationDbContext ApplicationDbContext{get;}

        public FilteredArticle(GadgetCMS.Data.ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }
        
        public FilteredArticle()
        {
            
        }

        public List<Data.ArticleParameter> getValues(string abc)
        {
            return ApplicationDbContext.ArticleParameter
                .Where(b => b.ParameterVal == abc).ToList();
        }
    }
}
