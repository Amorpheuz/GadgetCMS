using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.Data
{
    public class ArticlePicture
    {
        [Key]
        public int ArticlePictureId { get; set; }

        [Required]
        [DisplayName("Article Picture")]
        public byte[] ArticlePictureBytes { get; set; }

        [Required]
        [DisplayName("Caption")]
        public string ArticlePictureCaption { get; set; }

        [Required]
        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
    }
}
