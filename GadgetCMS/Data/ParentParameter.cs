using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.Data
{
    public class ParentParameter
    {
        [Key]
        public int ParentParameterId { get; set; }

        [Required]
        [DisplayName("Parent Parameter Name")]
        public  string ParentParameterName { get; set; }

        [Required]
        [DisplayName("Parent Parameter Description")]
        public string ParentParameterDescription { get; set; }

        public List<CategoryParentParameter> CategoryParentParameters { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}
