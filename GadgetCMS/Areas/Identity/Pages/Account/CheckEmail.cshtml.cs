using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetCMS.Areas.Identity.Pages.Account
{
    public class CheckEmailModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}