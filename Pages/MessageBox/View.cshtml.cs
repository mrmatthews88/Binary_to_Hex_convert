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
    public class ViewModel : PageModel
    {
        private readonly myWebApp.Models.myWebAppContext _context;

        public ViewModel(myWebApp.Models.myWebAppContext context)
        {
            _context = context;
        }

        public Topic Topic { get; set; }

        [BindProperty]
        public Reply Reply { get; set; }

        public async Task<IActionResult> OnGetAsync(int? TopicId)
        {
            if (TopicId == null)
            {
                return NotFound();
            }

            Topic = await _context
                .Topic
                .Include(t => t.Replies)
                .FirstOrDefaultAsync(t => t.ID == TopicId);
            Topic.Replies = Topic.Replies.OrderByDescending(r => r.ID).ToList();
            if (Topic == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? TopicId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Reply.MessageDate = DateTime.Now;

            _context.Reply.Add(Reply);
            await _context.SaveChangesAsync();

            return await OnGetAsync(TopicId);
        }
    }
}
