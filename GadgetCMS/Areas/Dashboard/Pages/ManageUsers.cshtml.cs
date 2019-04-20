using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Areas.Identity.Data;
using GadgetCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetCMS.Areas.Dashboard.Pages.ManageUsers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<GadgetCMSUser> userManager;
        private readonly GadgetCMSContext _context;
        private readonly RoleManager<IdentityRole> roleManager;

        public IndexModel(UserManager<GadgetCMSUser> userManager, GadgetCMSContext context, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        private IEnumerable<GadgetCMSUser> UsersOfRoleMember;
        private IEnumerable<GadgetCMSUser> UsersOfRoleEditor;
        private IEnumerable<GadgetCMSUser> UsersOfRoleModerator;
        private IEnumerable<GadgetCMSUser> UsersOfRoleAdmin;
        public List<UserWithRole> UserWithRoles;
        public IQueryable<IdentityRole> Roles;
        public string CurUserEmail;
        public IList<string> CurUserRole;

        public async Task OnGet()
        {
            UsersOfRoleMember = await userManager.GetUsersInRoleAsync("Member");
            UsersOfRoleEditor = await userManager.GetUsersInRoleAsync("Editor");
            UsersOfRoleModerator = await userManager.GetUsersInRoleAsync("Moderator");
            UsersOfRoleAdmin = await userManager.GetUsersInRoleAsync("Admin");

            UserWithRoles = new List<UserWithRole>();

            foreach (var item in UsersOfRoleMember)
            {
                UserWithRoles.Add(new UserWithRole {
                    UserId = item.Id,
                    UserEmail = item.Email,
                    UserRole = "Member",
                    UserBan = item.BanStatus
                });
            }
            foreach (var item in UsersOfRoleEditor)
            {
                UserWithRoles.Add(new UserWithRole
                {
                    UserId = item.Id,
                    UserEmail = item.Email,
                    UserRole = "Editor",
                    UserBan = item.BanStatus
                });
            }
            foreach (var item in UsersOfRoleModerator)
            {
                UserWithRoles.Add(new UserWithRole
                {
                    UserId = item.Id,
                    UserEmail = item.Email,
                    UserRole = "Moderator",
                    UserBan = item.BanStatus
                });
            }
            foreach (var item in UsersOfRoleAdmin)
            {
                UserWithRoles.Add(new UserWithRole
                {
                    UserId = item.Id,
                    UserEmail = item.Email,
                    UserRole = "Admin",
                    UserBan = item.BanStatus
                });
            }

            var temp = await userManager.GetUserAsync(HttpContext.User);

            CurUserEmail = temp.Email;
            CurUserRole = await userManager.GetRolesAsync(temp);

            Roles = roleManager.Roles;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string uRole = Request.Form["Roles"].ToString();
            string user = Request.Form["UserID"].ToString();
            string curRole = Request.Form["curRole"].ToString();

            GadgetCMSUser gadgetCmsUser = await userManager.FindByIdAsync(user);
            
            var removeResult = await userManager.RemoveFromRoleAsync(gadgetCmsUser, curRole);
            var addResult = await userManager.AddToRoleAsync(gadgetCmsUser, uRole);

            if (removeResult.Succeeded && addResult.Succeeded)
            {
                return RedirectToPage("./ManageUsers");
            }
            else if(removeResult.Succeeded && !addResult.Succeeded)
            {
                await userManager.AddToRoleAsync(gadgetCmsUser, curRole);
                return RedirectToPage("/Error");
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }

    public class UserWithRole
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public bool UserBan { get; set; }
    }
}