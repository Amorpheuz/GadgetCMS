using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetCMS.Areas.Dashboard.Pages
{
    public class DownloadNLogFilesModel : PageModel
    {
        public void OnGet()
        {
        }

        //public IActionResult OnPost()
        //{
        //    var net = new System.Net.WebClient();
        //    var data = net.DownloadData("Nlog");
        //    var content = new System.IO.MemoryStream(data);
        //    var contentType = "APPLICATION/octet-stream";
        //    var fileName = "nlog-own.txt";
        //    return File(content, contentType, fileName);
        //}
    }
}