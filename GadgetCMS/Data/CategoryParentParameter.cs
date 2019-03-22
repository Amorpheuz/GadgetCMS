using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.Data
{
    public class CategoryParentParameter
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ParentParameterId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("ParentParameterId")]
        public ParentParameter ParentParameter { get; set; }
    }
}
