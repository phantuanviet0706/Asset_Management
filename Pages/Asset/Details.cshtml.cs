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
    public class DetailsModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public DetailsModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }
        public Users userProfile { get; set; } = default!;
        public string Msg;

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
            userProfile = user;


            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }
            else 
            {
                if (_context.AssetLocations != null)
                {
                    asset.Location = await _context.AssetLocations.FirstOrDefaultAsync(m => m.Id == asset.LocationId);
                }

                if (_context.AssetStatuses != null)
                {
                    asset.Status = await _context.AssetStatuses.FirstOrDefaultAsync(m => m.Id == asset.StatusId);
                }

                if (_context.AssetTypes != null)
                {
                    asset.Type = await _context.AssetTypes.FirstOrDefaultAsync(m => m.Id == asset.TypeId);
                }

                if (_context.AssetVendors != null)
                {
                    asset.Vendor = await _context.AssetVendors.FirstOrDefaultAsync(m => m.Id == asset.VendorId);
                }

                Asset = asset;
            }
            return Page();
        }
    }
}
