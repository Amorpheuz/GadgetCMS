using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace GadgetCMS.Pages.Review
{
    public class EditModel : PageModel
    {
        private readonly GadgetCMS.Data.ApplicationDbContext _context;
        private readonly UserManager<GadgetCMSUser> _userManager;
        private readonly MLModelEngine<SentimentData, SentimentPrediction> _modelEngine;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        

        public EditModel(GadgetCMS.Data.ApplicationDbContext context, UserManager<GadgetCMSUser> userManager, MLModelEngine<SentimentData, SentimentPrediction> modelEngine)
        {
            _userManager = userManager;
            _context = context;
            _modelEngine = modelEngine;
        }

        [BindProperty]
        public Data.Review Review { get; set; }

        [BindProperty]
        public string ArticleName { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = _userManager.GetUserId(User);
            int idx = Int32.Parse(id);
            Review = await _context.Review.FindAsync(userId,idx);
            ViewData["ReviewContent"] = Review.ReviewContent;
            ArticleName = _context.Article.FirstOrDefault(a => a.ArticleId == Review.ArticleId).ArticleName;

            if (Review == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if(Review.ReviewContent != null)
                {
                    ViewData["ReviewContent"] = Review.ReviewContent;
                }
                return Page();
            }

            string evalText = Request.Form["editContent"];
            Review.ReviewType = OnGetPredictSentiment(evalText);
            _context.Attach(Review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                var user = await _userManager.GetUserAsync(User);
                logger.Info("{user} updated review on article carrying - id {id} on {date}",user.Email,Review.ArticleId,DateTime.Now);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(Review.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Article/Details",new { id = Review.ArticleId});
        }

        private bool ReviewExists(string id)
        {
            return _context.Review.Any(e => e.UserId == id);
        }
        private bool OnGetPredictSentiment(string SentimentText)
        {
            SentimentData sampleData = new SentimentData() { SentimentText = SentimentText };
            SentimentPrediction prediction = _modelEngine.Predict(sampleData);
            return prediction.Prediction;
        }
    }
}
