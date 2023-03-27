using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public CreateModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

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
            return Page();
        }
        public Users userProfile { get; set; } = default!;
        public string Msg;

        [BindProperty]
        public Users newUser { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
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

            if (!ModelState.IsValid || _context.Users == null || newUser == null)
            {
                return Page();
            }
            if (newUser.Username == null)
            {
                Msg = "Username can't be empty";
                ViewData["msg"] = Msg;
                return Page();
            }
            if (newUser.Password == null)
            {
                Msg = "Password can't be empty";
                ViewData["msg"] = Msg;
                return Page();
            }

            newUser.UserCode = "User";

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Setting/Users");
        }
    }
}
