﻿using System;
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

namespace GadgetCMS.Pages.Category
{
    [Authorize(Roles = "Admin,Moderator,Editor")]
    public class CreateModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        public string parentParamterIds = null;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<GadgetCMSUser> _userManager;
        public CreateModel(GadgetCMS.Data.ApplicationDbContext context,UserManager<GadgetCMSUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["ParentParameters"] = new SelectList(_context.Set<Data.ParentParameter>(), "ParentParameterId", "ParentParameterName");
            return Page();
        }

        [BindProperty]
        public Data.Category Category { get; set; }
        public Data.CategoryParentParameter CategoryParentParameter;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           _context.Category.Add(Category);
            await _context.SaveChangesAsync();
            
            int CategoryId = Category.CategoryId;

            parentParamterIds = Request.Form["AddParentParameters"];
            string[] parentParamterIdsArrayString = parentParamterIds.Split(",");
             int[] parentParamterIdsArrayInt = new int[parentParamterIdsArrayString.Length];
            for(int i=0;i<parentParamterIdsArrayString.Length;i++)
            {
                parentParamterIdsArrayInt[i] = Convert.ToInt32(parentParamterIdsArrayString[i].ToString());
            }

            foreach(var ppId in parentParamterIdsArrayInt)
            {
                CategoryParentParameter = new Data.CategoryParentParameter()
                {
                    CategoryId = CategoryId,
                    ParentParameterId = ppId
                };
                _context.CategoryParentParameter.Add(CategoryParentParameter);
                await _context.SaveChangesAsync();
            }
            var user = await _userManager.GetUserAsync(User);
            logger.Info("{user} created category {category} - carrying id {id} on {date}",user.Email,Category.CategoryName,Category.CategoryId,DateTime.Now);
            return RedirectToPage("../Categories");
        }
    }
}