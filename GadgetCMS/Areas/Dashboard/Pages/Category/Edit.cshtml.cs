﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;
using NLog;
using Microsoft.AspNetCore.Identity;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace GadgetCMS.Pages.Category
{
    [Authorize(Roles = "Admin,Moderator,Editor")]
    public class EditModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<GadgetCMSUser> _userManager;
        List<int> ppIds = new List<int>();
        public string parentParamterIds = null;
        List<Data.ParentParameter> ParentParameters = new List<Data.ParentParameter>();
        public List<List<Data.ParentParameter>> ParentParametersMain = new List<List<Data.ParentParameter>>();
        public EditModel(GadgetCMS.Data.ApplicationDbContext context,UserManager<GadgetCMSUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Data.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(m => m.CategoryId == id);

            ViewData["ParentParameters"] = new SelectList(_context.Set<Data.ParentParameter>(), "ParentParameterId", "ParentParameterName");

            ppIds = _context.CategoryParentParameter.Where(c => c.CategoryId == id).Select(d => d.ParentParameterId).ToList();
            foreach(var ppidsFE in ppIds)
            {
                ParentParameters = _context.ParentParameter.Where(c => c.ParentParameterId == ppidsFE).ToList();
                ParentParametersMain.Add(ParentParameters);
            }

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public Data.CategoryParentParameter CategoryParentParameter;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var ppRemoveIds = _context.CategoryParentParameter.Where(c => EF.Property<int>(c,"CategoryId") == Category.CategoryId);
            foreach(var ppRemoveIdsFE in ppRemoveIds)
            {
                _context.CategoryParentParameter.Remove(ppRemoveIdsFE);
            }

            _context.Attach(Category).State = EntityState.Modified;

            int CategoryId = Category.CategoryId;

            parentParamterIds = Request.Form["AddParentParameters"];

            if(parentParamterIds != null)
            {
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

            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var user = await _userManager.GetUserAsync(User);
            logger.Info("{user} edited category {category} - carrying {id} on {date}",user.Email,Category.CategoryName,Category.CategoryId,DateTime.Now);
            return RedirectToPage("../Categories");
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
