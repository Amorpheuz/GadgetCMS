using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetCMS.Areas.Dashboard.Pages
{
    [Authorize(Roles= "Admin,Moderator,Editor")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}