using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;
using System.Text.RegularExpressions;

namespace Project_PRN.Pages.AssetVendor
{
    public class CreateModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public CreateModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }
        public Users userProfile { get; set; } = default!;

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

        [BindProperty]
        public Vendors AssetVendor { get; set; } = default!;
        public string Msg;


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

            if (!ModelState.IsValid || _context.AssetVendors == null || AssetVendor == null)
            {
                return Page();
            }

            if (AssetVendor.Name == null)
            {
                Msg = "Vendor Name can't be empty";
                ViewData["msg"] = Msg;
                return Page();
            }

            if (AssetVendor.Phone != null)
            {
                if (!Regex.IsMatch(AssetVendor.Phone, "^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$"))
                {
                    Msg = "Invalid Phone number";
                    ViewData["msg"] = Msg;
                    return Page();

                }
            }

            if (AssetVendor.Email != null)
            {
                if (!Regex.IsMatch(AssetVendor.Email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$"))
                {
                    Msg = "Invalid Email";
                    ViewData["msg"] = Msg;
                    return Page();

                }
            }

            _context.AssetVendors.Add(AssetVendor);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Setting/Vendors");
        }
    }
}
