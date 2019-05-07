using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GadgetCMS.Data;
using NLog;
using Microsoft.AspNetCore.Identity;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace GadgetCMS.Pages.ParentParameter
{
    [Authorize(Roles = "Admin,Moderator,Editor")]
    public class CreateModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<GadgetCMSUser> _userManager;
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
            var user = await _userManager.GetUserAsync(User);
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
            logger.Info("{user} added ParentParameter {ppName} carrying - id {id} on {date}",user.Email,ParentParameter.ParentParameterName,ParentParameter.ParentParameterId,DateTime.Now);
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
                    logger.Info("{user} added Parameter {pName} under ParentParameter {ppname} carrying -id {id} on {date}",user.Email,ParameterName.ElementAt(i),ParentParameter.ParentParameterName,ppId,DateTime.Now);
                }

                await _context.SaveChangesAsync();
            }

            
            
            return RedirectToPage("../Parameters");
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