using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.Asset
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
        public Assets Asset { get; set; } = default!;

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

            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset =  await _context.Assets.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }
            Asset = asset;
            ViewData["LocationId"] = new SelectList(_context.AssetLocations, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.AssetStatuses, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.AssetTypes, "Id", "Name");
            ViewData["VendorId"] = new SelectList(_context.AssetVendors, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["LocationId"] = new SelectList(_context.AssetLocations, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.AssetStatuses, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.AssetTypes, "Id", "Name");
            ViewData["VendorId"] = new SelectList(_context.AssetVendors, "Id", "Name");
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

            if (Asset.Name == null || Asset.Name.ToString().Trim() == "")
            {
                Msg = "Please input Asset Name";
                ViewData["msg"] = Msg;
                return Page();
            }

            if (Asset.LocationId == null)
            {
                Msg = "Please choose a Location";
                ViewData["msg"] = Msg;
                return Page();
            }


            _context.Attach(Asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(Asset.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AssetExists(int id)
        {
          return (_context.Assets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
