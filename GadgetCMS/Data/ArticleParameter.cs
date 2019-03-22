using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.Data
{
    public class ArticleParameter
    {
        [Required]
        public int ArticleId { get; set; }

        [Required]
        public int ParameterId { get; set; }

        [Required]
        public string ParameterVal { get; set; }

        [ForeignKey("ArticleId")]
        public Article Article { get; set; }

        [ForeignKey("ParameterId")]
        public Parameter Parameter { get; set; }
    }
}
