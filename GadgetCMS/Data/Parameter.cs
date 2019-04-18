using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Paramerter Name")]
        public string ParameterName { get; set; }

        [Required]
        [DisplayName("Paramerter Description")]
        public string ParameterDescription { get; set; }

        [Required]
        [DisplayName("Paramerter Unit")]
        public string ParameterUnit { get; set; }

        [Required]
        public int ParentParameterId { get; set; }

        [ForeignKey("ParentParameterId")]
        [DisplayName("Parent Paramerter")]
        public ParentParameter ParentParameter { get; set; }

        public List<ArticleParameter> ArticleParameters { get; set; }
    }
}
