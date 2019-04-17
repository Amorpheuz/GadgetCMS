using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Areas.Identity.Data;

namespace GadgetCMS.Data
{
    public class Review
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [DisplayName("Article Name")]
        public int ArticleId { get; set; }

        [Required]
        [DisplayName("Rating")]
        public double ReviewRating { get; set; }

        [Required]
        [DisplayName("Content")]
        public string ReviewContent { get; set; }
        
        [DisplayName("Last Updated On")]
        public DateTime ReviewLastUpdate { get; set; }

        [Required]
        [DisplayName("Created On")]
        public DateTime ReviewCreated { get; set; }

        [Required]
        public bool ReviewVisible { get; set; }

        [Required]
        [DisplayName("Review Title")]
        public string ReviewTitle { get; set; }

        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
        [ForeignKey("UserId")]
        [DisplayName("User")]
        public GadgetCMSUser GadgetCmsUser { get; set; }

    }
}
