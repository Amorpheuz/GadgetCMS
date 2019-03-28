﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GadgetCMS.Data;

namespace GadgetCMS.Pages.ArticleParameter
{
    public class CreateModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;

        public CreateModel(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ArticleId"] = new SelectList(_context.Article, "ArticleId", "ArticleName");
            ViewData["ParameterId"] = new SelectList(_context.Parameter, "ParameterId", "ParameterName");
            return Page();
        }

        [BindProperty]
        public Data.ArticleParameter ArticleParameter { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ArticleParameter.Add(ArticleParameter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}