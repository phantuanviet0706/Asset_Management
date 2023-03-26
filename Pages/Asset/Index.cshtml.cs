using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.Asset
{
    public class IndexModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public IndexModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }
        public Users userProfile { get; set; } = default!;

        public IList<Assets> Asset { get; set; } = default!;

        public const int INACTIVE_STATUS = 1;

        public async Task<IActionResult> OnGetAsync()
        {
            string username = HttpContext.Session.GetString("username");
            if (username == null || _context.Users == null)
            {
                return RedirectToPage("/Login");
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return RedirectToPage("/Login");
            }
            userProfile = user;

            if (user.UserCode.ToLower() != "admin")
            {
                if (_context.Assets != null)
                {
                    Asset = await _context.Assets
                    .Include(a => a.Assignee)
                    .Include(a => a.CreateByUserNavigation)
                    .Include(a => a.Location)
                    .Include(a => a.Status)
                    .Include(a => a.Type)
                    .Include(a => a.Vendor)
                    .Where(a => a.AssigneeId == userProfile.Id)
                    .ToListAsync();
                }
                return Page();
            }
            else
            {
                if (_context.Assets != null)
                {
                    Asset = await _context.Assets
                    .Include(a => a.Assignee)
                    .Include(a => a.CreateByUserNavigation)
                    .Include(a => a.Location)
                    .Include(a => a.Status)
                    .Include(a => a.Type)
                    .Include(a => a.Vendor).ToListAsync();
                }
                return Page();
            }
        }
    }
}
