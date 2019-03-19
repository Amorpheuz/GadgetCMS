using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GadgetCMS.Data;
using Microsoft.AspNetCore.Identity;

namespace GadgetCMS.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the GadgetCMSUser class
    public class GadgetCMSUser : IdentityUser
    {
        [PersonalData]
        public string Nickname { get; set; }
        
        [Required]
        public bool BanStatus { get; set; }
        
        [Required]
        public bool StarReview { get; set; }

        [PersonalData]
        public List<ArticleLog> ArticleLogs { get; set; }

        [PersonalData]
        public List<Review> Reviews { get; set; }
    }
}
