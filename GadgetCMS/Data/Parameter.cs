using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.Data
{
    public class Parameter
    {
        [Key]
        public int ParameterId { get; set; }

        [Required]
        public string ParameterName { get; set; }

        [Required]
        public string ParameterDescription { get; set; }

        [Required]
        public string ParameterUnit { get; set; }

        [Required]
        public int ParentParameterId { get; set; }

        [ForeignKey("ParentParameterId")]
        public ParentParameter ParentParameter { get; set; }

        public List<ArticleParameter> ArticleParameters { get; set; }
    }
}
