using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using reCAPTCHA.AspNetCore;
using NLog;

namespace GadgetCMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<GadgetCMSUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private IRecaptchaService _recaptcha;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public LoginModel(SignInManager<GadgetCMSUser> signInManager, ILogger<LoginModel> logger, IRecaptchaService recaptcha)
        {
            _signInManager = signInManager;
            _logger = logger;
            _recaptcha = recaptcha;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var recaptcha = await _recaptcha.Validate(Request);
            if (!recaptcha.success)
                ModelState.AddModelError("Recaptcha", "There was an error validating recaptcha. Please try again!");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                var user = _signInManager.UserManager.FindByEmailAsync(Input.Email).Result;
                if (result.Succeeded)
                {
                    if(user.BanStatus == true)
                    {
                        await _signInManager.SignOutAsync();
                        _logger.LogInformation("User logged out.");
                        logger.Info("{user} logged out with BanStatus - True",Input.Email);
                        ModelState.AddModelError(string.Empty, "User is Banned.");
                        return Page();
                    }
                    _logger.LogInformation("User logged in.");
                    logger.Info("{user} logged in",Input.Email);
                    return LocalRedirect(returnUrl);
                }
                if (user != null)
                {
                    if (!_signInManager.UserManager.IsEmailConfirmedAsync(user).Result)
                    {
                        // Clear the existing external cookie to ensure a clean login process
                        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                        ViewData["unVerifiedMail"] = true;
                        return Page();
                    }
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    // Clear the existing external cookie to ensure a clean login process
                    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return Page();
        }
    }
}
