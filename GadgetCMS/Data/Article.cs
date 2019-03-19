using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.Data
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        public  string ArticleName { get; set; }
        

        public DateTime ArticleLastUpdate { get; set; }

        [Required]
        public  DateTime ArticleCreated { get; set; }

        [Required]
        public string ArticleContent { get; set; }

        [Required]
        public  string ArticleAuthor { get; set; }

        [Required]
        public  string ArticleLastEditedBy { get; set; }

        [Required]
        public bool ArticleVisible { get; set; }

        [Required]
        public bool ArticleEditLock { get; set; }

        public List<ArticleLog> ArticleLogs { get; set; }
    }
}
