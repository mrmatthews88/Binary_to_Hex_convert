using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using myWebApp.Models;

namespace myWebApp.Pages.MessageBox
{
    public class IndexModel : PageModel
    {
        private readonly myWebApp.Models.myWebAppContext _context;

        public IndexModel(myWebApp.Models.myWebAppContext context)
        {
            _context = context;
        }

        public IList<Topic> Topics { get;set; }

        [BindProperty]
        public Topic Topic { get;set; }

        public async Task OnGetAsync()
        {
            Topics = await _context.Topic.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int? TopicId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Topic.Add(Topic);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
