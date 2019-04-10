using GadgetCMS.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.ViewComponents
{
    public class CompanyNamesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CompanyNamesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<string> CompanyNames { get; set; }

        public IViewComponentResult Invoke()
        {
            CompanyNames = _context.ArticleParameter
                .Where(a => a.ParameterId == 5).Select(a => a.ParameterVal).Distinct().ToList();

            return View(CompanyNames);
        }
    }
}
