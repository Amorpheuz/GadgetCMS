using System;
using System.Collections.Generic;
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
        public  string ParentParameterName { get; set; }

        [Required]
        public string ParentParameterDescription { get; set; }

        public List<CategoryParentParameter> CategoryParentParameters { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}
