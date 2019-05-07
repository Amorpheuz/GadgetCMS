using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;
using NLog;
using Microsoft.AspNetCore.Identity;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace GadgetCMS.Pages.ParentParameter
{
    [Authorize(Roles = "Admin,Moderator,Editor")]
    public class DeleteModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<GadgetCMSUser> _userManager;
        public DeleteModel(GadgetCMS.Data.ApplicationDbContext context,UserManager<GadgetCMSUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Data.ParentParameter ParentParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParentParameter = await _context.ParentParameter.Include(m => m.Parameters).FirstOrDefaultAsync(m => m.ParentParameterId == id);

            if (ParentParameter == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParentParameter = await _context.ParentParameter.FindAsync(id);

            if (ParentParameter != null)
            {
                _context.ParentParameter.Remove(ParentParameter);
                await _context.SaveChangesAsync();
            }
            var user = await _userManager.GetUserAsync(User);
            logger.Info("{user} deleted ParentParameter {ppName} carrying - id {ppId} on {date}",user.Email,ParentParameter.ParentParameterName,ParentParameter.ParentParameterId,DateTime.Now);
            return RedirectToPage("../Parameters");
        }
    }
}
