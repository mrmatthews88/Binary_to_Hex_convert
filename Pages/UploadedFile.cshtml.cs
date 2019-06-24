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
using myWebApp.Models;

namespace myWebApp.Pages
{
    public class UploadedFileModel : PageModel
    {
        private readonly IHostingEnvironment env;
        public UploadedFileModel(IHostingEnvironment _env) { env = _env; }

        public IFileInfo file;
        public Paginator paginator = new Paginator();
        public string basePath;

        [BindProperty]
        public string[] bytes { get; set; }

        public void OnGet(string _file, int pageNum) => getPage(_file, pageNum);

        public void OnPost(string _file, int pageNum)
        {
            getPage(_file, pageNum);
            paginator.Update(bytes);
        }

        public void getPage(string _file, int pageNum)
        {
            basePath = $"/UploadedFile/{_file}";
            file = env.WebRootFileProvider.GetDirectoryContents("uploads")
                .First(f => f.Name == _file);

            paginator.LoadFile(file, pageNum);
        }

        public string ToAscii (string input)
        {
            return System.Convert.ToChar(System.Convert.ToUInt32(input, 16)).ToString();
        }

    }
}
