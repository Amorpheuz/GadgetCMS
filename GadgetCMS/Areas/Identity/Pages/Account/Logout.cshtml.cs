using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NLog;

namespace GadgetCMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<GadgetCMSUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public LogoutModel(SignInManager<GadgetCMSUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var user = await _signInManager.UserManager.FindByNameAsync(User.Identity.Name);

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            logger.Info("{user} logged out",user.Email);
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
    }
}