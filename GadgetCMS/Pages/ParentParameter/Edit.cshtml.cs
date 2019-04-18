using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ParentParameter
{
    public class EditModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public EditModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.ParentParameter ParentParameter { get; set; }

        [BindProperty]
        public List<Data.Parameter> Parameters { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParentParameter = await _context.ParentParameter.Include(p => p.Parameters).FirstOrDefaultAsync(m => m.ParentParameterId == id);

            Parameters = ParentParameter.Parameters;

            if (ParentParameter == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(List<string> ParameterName, List<string> ParameterDescription, List<string> ParameterUnit, List<string> DelParams)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ParameterName.Count != ParameterDescription.Count || ParameterName.Count != ParameterUnit.Count)
            {
                InitParameter(ParameterName, ParameterDescription, ParameterUnit);
                return Page();
            }
            for (int i = 0; i < ParameterName.Count; i++)
            {
                if (ParameterName.ElementAt(i) == "" || ParameterDescription.ElementAt(i) == "" || ParameterUnit.ElementAt(i) == "")
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

            _context.Update(ParentParameter);
            _context.UpdateRange(Parameters);

            try
            {
                await _context.SaveChangesAsync();
                if (DelParams.Count > 0)
                {
                    foreach (var item in DelParams)
                    {
                        if (item != null || item != "")
                        {
                            var temp = _context.Parameter.Where(p => p.ParameterId == Convert.ToInt32(item)).Single();
                            _context.Parameter.Remove(temp);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
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
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentParameterExists(ParentParameter.ParentParameterId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ParentParameterExists(int id)
        {
            return _context.ParentParameter.Any(e => e.ParentParameterId == id);
        }

        private void InitParameter(List<string> pName, List<string> pDesc, List<string> pUnit)
        {
            ViewData["PError"] = "Please fill all the Parameter Details";
            ViewData["PName"] = pName;
            ViewData["PDesc"] = pDesc;
            ViewData["PUnit"] = pUnit;
        }
    }
}
