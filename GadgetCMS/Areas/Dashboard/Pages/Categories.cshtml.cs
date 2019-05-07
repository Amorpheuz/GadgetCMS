using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GadgetCMS.Areas.Dashboard.Pages
{
    [Authorize(Roles = "Admin,Moderator,Editor")]
    public class CategoriesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CategoriesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<Data.Category> Categories { get; set; }

        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            var tempCategories = await _context.CategoryParentParameter
                .Include(p => p.Category)
                .Include(p => p.ParentParameter)
                .ToListAsync();

            var sorter = tempCategories.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                sorter = sorter.Where(s => s.ParentParameter.ParentParameterName.ToLower().Contains(searchString)
                                        || s.Category.CategoryName.ToLower().Contains(searchString)
                                        );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sorter = sorter.OrderByDescending(s => s.Category.CategoryName);
                    break;
                default:
                    sorter = sorter.OrderBy(s => s.Category.CategoryName);
                    break;
            }

            int pageSize = 7;
            var temp = sorter.Select(s => s.Category).Distinct().AsQueryable();
            Categories = PaginatedList<Data.Category>.Create(
                temp.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}