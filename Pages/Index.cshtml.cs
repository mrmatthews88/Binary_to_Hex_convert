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
    public class IndexModel : PageModel
    {
        private readonly IHostingEnvironment env;
        public IndexModel(IHostingEnvironment _env) { env = _env; }


        public void OnGet()
        {
        }

    }
}
