using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetCMS.Areas.Identity.Pages.Account
{
    public class ResendConfirmEmailModel : PageModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string UserEmail { get; set; }

        private readonly IEmailSender _emailSender;
        private readonly UserManager<GadgetCMSUser> _userManager;

        public ResendConfirmEmailModel(UserManager<GadgetCMSUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string UserEmail)
        {
            if (UserEmail == null)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(UserEmail);
            if (user == null)
            {
                ViewData["Error"] = "User with given Email not found.";
                return Page();
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = user.Id, code = code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(UserEmail, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            ViewData["flag"] = 1;
            return Page();
        }
    }
}