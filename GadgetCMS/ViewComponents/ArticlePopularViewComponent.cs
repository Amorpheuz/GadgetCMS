﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GadgetCMS.ViewComponents
{
    public class ArticlePopularViewComponent : ViewComponent
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        public Data.Article articlesPopular = new Data.Article();
        public List<Data.Article> articlesPopularParent = new List<Data.Article>();
        public List<int> reviews = new List<int>();
        
        public ArticlePopularViewComponent(GadgetCMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var reviewsVar = _context.Review.GroupBy(c => c.ArticleId).OrderByDescending(gp => gp.Count()).ToList();
            foreach (var reviewsFE in reviewsVar)
            {
                string articleId = reviewsFE.Select(cg => cg.ArticleId).FirstOrDefault().ToString();
                int articleIdInt = Int32.Parse(articleId);
                reviews.Add(articleIdInt);
            }

            foreach (var articles in reviews)
            {
                articlesPopular = _context.Article.Where(cg => cg.ArticleId == articles).FirstOrDefault();
                articlesPopularParent.Add(articlesPopular);
            }
            return View("Default", articlesPopularParent);
        }
    }
}