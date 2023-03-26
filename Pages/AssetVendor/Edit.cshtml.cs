using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetVendor
{
    public class EditModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public EditModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }
        public Users userProfile { get; set; } = default!;
        public string Msg;

        [BindProperty]
        public Vendors AssetVendor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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

            if (id == null || _context.AssetVendors == null)
            {
                return NotFound();
            }

            var assetvendor =  await _context.AssetVendors.FirstOrDefaultAsync(m => m.Id == id);
            if (assetvendor == null)
            {
                return NotFound();
            }
            AssetVendor = assetvendor;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

            if (!ModelState.IsValid)
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

            _context.Attach(AssetVendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetVendorExists(AssetVendor.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Setting/Vendors");
        }

        private bool AssetVendorExists(int id)
        {
          return (_context.AssetVendors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
