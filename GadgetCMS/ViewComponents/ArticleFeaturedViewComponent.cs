﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.ViewComponents
{
    public class ArticleFeaturedViewComponent : ViewComponent
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        public List<Data.Article> articlesList = new List<Data.Article>();
        public ArticleFeaturedViewComponent(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            articlesList = _context.Article.ToList();
            return View("Default",articlesList);
        }
    }
}