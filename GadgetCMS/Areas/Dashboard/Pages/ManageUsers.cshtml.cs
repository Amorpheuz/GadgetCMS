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
using Microsoft.EntityFrameworkCore;

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
        public UserPaginatedList<UserWithRole> UserWithRoles;
        public IQueryable<IdentityRole> Roles;
        public string CurUserEmail;
        public IList<string> CurUserRole;

        public string EmailSort { get; set; }
        public string RoleSort { get; set; }
        public string BanSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGet(string sortOrder,string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            EmailSort = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            RoleSort = sortOrder == "Role" ? "role_desc" : "Role";
            BanSort = sortOrder == "Ban" ? "ban_desc" : "Ban";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            UsersOfRoleMember = await userManager.GetUsersInRoleAsync("Member");
            UsersOfRoleEditor = await userManager.GetUsersInRoleAsync("Editor");
            UsersOfRoleModerator = await userManager.GetUsersInRoleAsync("Moderator");
            UsersOfRoleAdmin = await userManager.GetUsersInRoleAsync("Admin");

            var UwR = new List<UserWithRole>();

            foreach (var item in UsersOfRoleMember)
            {
                UwR.Add(new UserWithRole {
                    UserId = item.Id,
                    UserEmail = item.Email,
                    UserRole = "Member",
                    UserBan = item.BanStatus
                });
            }
            foreach (var item in UsersOfRoleEditor)
            {
                UwR.Add(new UserWithRole
                {
                    UserId = item.Id,
                    UserEmail = item.Email,
                    UserRole = "Editor",
                    UserBan = item.BanStatus
                });
            }
            foreach (var item in UsersOfRoleModerator)
            {
                UwR.Add(new UserWithRole
                {
                    UserId = item.Id,
                    UserEmail = item.Email,
                    UserRole = "Moderator",
                    UserBan = item.BanStatus
                });
            }
            foreach (var item in UsersOfRoleAdmin)
            {
                UwR.Add(new UserWithRole
                {
                    UserId = item.Id,
                    UserEmail = item.Email,
                    UserRole = "Admin",
                    UserBan = item.BanStatus
                });
            }

            var sort = UwR.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                sort = sort.Where(s => s.UserEmail.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "email_desc":
                    sort = sort.OrderByDescending(s => s.UserEmail);
                    break;
                case "Role":
                    sort = sort.OrderBy(s => s.UserRole);
                    break;
                case "role_desc":
                    sort = sort.OrderByDescending(s => s.UserRole);
                    break;
                case "Ban":
                    sort = sort.OrderBy(s => s.UserBan);
                    break;
                case "ban_desc":
                    sort = sort.OrderByDescending(s => s.UserBan);
                    break;
                default:
                    sort = sort.OrderBy(s => s.UserEmail);
                    break;
            }

            int pageSize = 5;
            UserWithRoles = UserPaginatedList<UserWithRole>.Create(
                sort.AsNoTracking(), pageIndex ?? 1, pageSize);

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

        public async Task<IActionResult> OnPostBanUserAsync()
        {
            string userId = Request.Form["UserID"].ToString();

            var reqUser = await userManager.GetUserAsync(HttpContext.User);
            var reqUserRole = await userManager.GetRolesAsync(reqUser);

            if (reqUserRole.Contains("Admin") || reqUserRole.Contains("Moderator"))
            {
                var user = await _context.Users.FindAsync(userId);
                user.BanStatus = true;
                await userManager.UpdateAsync(user);
                return RedirectToPage("./ManageUsers");
            }
            return RedirectToPage("./ManageUsers");
        }

        public async Task<IActionResult> OnPostUnbanUserAsync()
        {
            string userId = Request.Form["UserID"].ToString();

            var reqUser = await userManager.GetUserAsync(HttpContext.User);
            var reqUserRole = await userManager.GetRolesAsync(reqUser);

            if (reqUserRole.Contains("Admin") || reqUserRole.Contains("Moderator"))
            {
                var user = await _context.Users.FindAsync(userId);
                user.BanStatus = false;
                await userManager.UpdateAsync(user);
                return RedirectToPage("./ManageUsers");
            }
            return RedirectToPage("./ManageUsers");
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