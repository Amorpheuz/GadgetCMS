using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetCMS.Areas.Dashboard.Pages
{
    [Authorize(Roles = "Admin")]
    public class DownloadNLogFilesModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string startPath = @".\wwwroot\Nlog";
            string zipPath = @".\wwwroot\LogZip\Logs.zip";
            var filename = "Logs.zip";

            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LogZip", filename)))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LogZip", filename));
            }

            ZipFile.CreateFromDirectory(startPath,zipPath);

            
            var path = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot",
                           "LogZip", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/zip", Path.GetFileName(path));
        }
    }
}