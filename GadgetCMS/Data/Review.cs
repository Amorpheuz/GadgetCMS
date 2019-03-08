using System;
using System.Collections.Generic;
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
        public int ArticleId { get; set; }

        [Required]
        public double ReviewRating { get; set; }

        [Required]
        public string ReviewContent { get; set; }

        [Required]
        public DateTime ReviewLastUpdate { get; set; }

        [Required]
        public DateTime ReviewCreated { get; set; }

        [Required]
        public bool ReviewVisible { get; set; }


        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
        [ForeignKey("UserId")]
        public GadgetCMSUser GadgetCmsUser { get; set; }

    }
}
