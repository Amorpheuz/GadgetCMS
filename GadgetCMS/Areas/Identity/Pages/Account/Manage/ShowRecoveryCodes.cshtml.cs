using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetCMS.Areas.Identity.Pages.Account.Manage
{
    public class ShowRecoveryCodesModel : PageModel
    {
        public string[] RecoveryCodes { get; set; }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var recoveryCodes = (string[])TempData["RecoveryCodes"];
            if (recoveryCodes == null)
            {
                return RedirectToPage("./EnableAuthenticator");
            }

            RecoveryCodes = recoveryCodes;
            return Page(); 
        }
    }
}