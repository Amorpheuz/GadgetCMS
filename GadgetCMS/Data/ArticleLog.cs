using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Areas.Identity.Data;

namespace GadgetCMS.Data
{
    public class ArticleLog
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public int ArticleId { get; set; }

        [Required]
        public string ArticleLogType { get; set; }

        [Required]
        public string ArticleLogContent { get; set; }

        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
        [ForeignKey("UserId")]
        public GadgetCMSUser GadgetCmsUser { get; set; }
    }
}
