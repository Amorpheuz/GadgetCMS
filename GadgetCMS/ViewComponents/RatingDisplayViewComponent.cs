using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GadgetCMS.ViewComponents
{
    public class RingControlModel
    {
        public List<SelectListItem> SelectedListItems { get; set; }
        public string RatingControlValue { get; set; }
        public string RatingControlIdValue { get; internal set; }
    }

    public class RatingDisplayViewComponent : ViewComponent
    {
        private readonly GadgetCMSContext _context;

        public RatingDisplayViewComponent(GadgetCMSContext context)
        {
            _context = context;
        }
        
        public IViewComponentResult Invoke(int articleId, string userId)
        {
            if(userId == "")
            {
                var ratingControlValues = new List<string>();
                var ratingControlInitialValue = "";

                ratingControlValues.Add("");
                ratingControlValues.Add("1");
                ratingControlValues.Add("2");
                ratingControlValues.Add("3");
                ratingControlValues.Add("4");
                ratingControlValues.Add("5");

                ratingControlInitialValue = _context.Article.Where(r => r.ArticleId == articleId).Select(r => r.ArticleRating).First().ToString();

                List<SelectListItem> ratings = ratingControlValues.Select(
                    myValue => new SelectListItem
                    {
                        Value = myValue,
                        Text = myValue
                    }).ToList();

                RingControlModel ratingControlModel = new RingControlModel
                {
                    SelectedListItems = ratings,
                    RatingControlValue = ratingControlInitialValue,
                    RatingControlIdValue = "rating" + articleId
                };

                return View(ratingControlModel);
            }
            else
            {
                var ratingControlValues = new List<string>();
                var ratingControlInitialValue = "";

                ratingControlValues.Add("");
                ratingControlValues.Add("1");
                ratingControlValues.Add("2");
                ratingControlValues.Add("3");
                ratingControlValues.Add("4");
                ratingControlValues.Add("5");

                ratingControlInitialValue = _context.Review.Where(r => r.ArticleId == articleId && r.UserId == userId).Select(r => r.ReviewRating).First().ToString();

                List<SelectListItem> ratings = ratingControlValues.Select(
                    myValue => new SelectListItem
                    {
                        Value = myValue,
                        Text = myValue
                    }).ToList();

                var tempId = userId.Substring(0,8);

                RingControlModel ratingControlModel = new RingControlModel
                {
                    SelectedListItems = ratings,
                    RatingControlValue = ratingControlInitialValue,
                    RatingControlIdValue = "rating" + articleId + tempId
                };

                return View(ratingControlModel);
            }
        }
    }
}
