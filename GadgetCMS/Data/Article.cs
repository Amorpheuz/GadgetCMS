using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.Data
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        [DisplayName("Article Title")]
        public  string ArticleName { get; set; }

        [DisplayName("Last Updated On")]
        public DateTime ArticleLastUpdate { get; set; }

        [Required]
        [DisplayName("Created On")]
        public  DateTime ArticleCreated { get; set; }

        [Required]
        [DisplayName("Content")]
        public string ArticleContent { get; set; }

        [Required]
        [DisplayName("Author")]
        public  string ArticleAuthor { get; set; }

        [Required]
        [DisplayName("Last Edited By")]
        public  string ArticleLastEditedBy { get; set; }

        public bool? ArticleVisible { get; set; }

        public bool? ArticleEditLock { get; set; }

        [Range(0,5)]
        [DisplayName("Rating")]
        public double ArticleRating { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public List<ArticleLog> ArticleLogs { get; set; }
        public List<Review> Reviews { get; set; }
        public List<ArticleParameter> ArticleParameters { get; set; }
        public List<ArticlePicture> ArticlePictures { get; set; }
    }
}
