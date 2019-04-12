using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.Data
{
    public class FilteredArticle
    {
        //public int FilteredArticleId {get;set;}
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
