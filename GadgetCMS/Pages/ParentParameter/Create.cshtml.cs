using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ParentParameter
{
    public class CreateModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public CreateModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Data.ParentParameter ParentParameter { get; set; }

        public async Task<IActionResult> OnPostAsync(List<string> ParameterName, List<string> ParameterDescription, List<string> ParameterUnit)
        {
            if (!ModelState.IsValid)
            {
                InitParameter(ParameterName,ParameterDescription,ParameterUnit);
                return Page();
            }

            if(ParameterName.Count != ParameterDescription.Count || ParameterName.Count != ParameterUnit.Count)
            {
                InitParameter(ParameterName, ParameterDescription, ParameterUnit);
                return Page();
            }
            for (int i = 0; i < ParameterName.Count; i++)
            {
                if (ParameterName.ElementAt(i)== "" || ParameterDescription.ElementAt(i) == "" || ParameterUnit.ElementAt(i) == "")
                {
                    InitParameter(ParameterName, ParameterDescription, ParameterUnit);
                    return Page();
                }
                if (ParameterName.ElementAt(i) == null || ParameterDescription.ElementAt(i) == null || ParameterUnit.ElementAt(i) == null)
                {
                    InitParameter(ParameterName, ParameterDescription, ParameterUnit);
                    return Page();
                }
            }


            _context.ParentParameter.Add(ParentParameter);
            await _context.SaveChangesAsync();

            if (ParameterName.Count != 0)
            {
                var ppId = ParentParameter.ParentParameterId;
                for (int i = 0; i < ParameterName.Count; i++)
                {
                    _context.Parameter.Add(
                            new Data.Parameter
                            {
                                ParameterDescription = ParameterDescription.ElementAt(i),
                                ParameterName = ParameterName.ElementAt(i),
                                ParameterUnit = ParameterUnit.ElementAt(i),
                                ParentParameterId = ppId
                            }
                        );
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private void InitParameter(List<string> pName, List<string> pDesc, List<string> pUnit)
        {
            ViewData["PError"]= "Please fill all the Parameter Details";
            ViewData["PName"] = pName;
            ViewData["PDesc"] = pDesc;
            ViewData["PUnit"] = pUnit;
        }
    }
}