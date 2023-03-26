using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;
using System.Collections;

namespace Project_PRN.Pages.Setting
{
    public class IndexModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public IndexModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

        public Users userProfile { get; set; } = default!;
        public IList<Locations> locations { get; set; } = default!;

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
            if (user.UserCode.ToLower() != "admin")
            {
                return RedirectToPage("/Login");
            }
            userProfile = user;
            
            if (_context.AssetLocations != null)
            {
                locations = await _context.AssetLocations.ToListAsync();
            }
            return Page();
        }
    }
}
