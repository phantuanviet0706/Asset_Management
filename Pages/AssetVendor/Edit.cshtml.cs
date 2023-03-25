using System;
using System.Collections.Generic;
using System.Linq;
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

        [BindProperty]
        public Vendors AssetVendor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
            if (!ModelState.IsValid)
            {
                return Page();
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

            return RedirectToPage("./Index");
        }

        private bool AssetVendorExists(int id)
        {
          return (_context.AssetVendors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
