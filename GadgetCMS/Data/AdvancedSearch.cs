using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.Data
{
    public class AdvancedSearch
    {
        public int AdvancedSearchId {get;set;}

        public List<Data.Article> Articles{get;set;}
        public List<Data.Parameter> Parameters {get;set;}
       
        public List<Data.ArticleParameter> ArticleParameters { get; set; }
    }
}
