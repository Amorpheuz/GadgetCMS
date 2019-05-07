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
    public class ParametersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ParametersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<ParentParameter> ParentParameters { get; set; }

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

            var tempParameters = await _context.ParentParameter
                .Include(p => p.Parameters)
                .ToListAsync();

            var sorter = tempParameters.SelectMany(tp => tp.Parameters).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                sorter = sorter.Where(s => s.ParameterName.ToLower().Contains(searchString) 
                                        || s.ParentParameter.ParentParameterName.ToLower().Contains(searchString)
                                        );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sorter = sorter.OrderByDescending(s => s.ParentParameter.ParentParameterName);
                    break;
                default:
                    sorter = sorter.OrderBy(s => s.ParentParameter.ParentParameterName);
                    break;
            }

            int pageSize = 7;
            var temp = sorter.Select(s => s.ParentParameter).Distinct().AsQueryable();
            ParentParameters = PaginatedList<ParentParameter>.Create(
                temp.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}