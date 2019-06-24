using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;

namespace myWebApp.Pages
{
    public class HexModel : PageModel
    {
        private readonly IHostingEnvironment env;
        public HexModel(IHostingEnvironment _env) { env = _env; }

        public IDirectoryContents files;

        [Display(Name = "uploadFile")]
        public IFormFile UploadFile { get; set; }

        public void OnGet()
        {
            files = env.WebRootFileProvider.GetDirectoryContents("uploads");
        }

        public void OnPost()
        {
            if (UploadFile != null)
            {
                string path = Path.Combine(env.WebRootPath, "uploads", UploadFile.FileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    UploadFile.CopyToAsync(fileStream);
                }
                files = env.WebRootFileProvider.GetDirectoryContents("uploads");
            }
        }
    }
}
