using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetLocation
{
    public class EditModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public EditModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Locations AssetLocation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AssetLocations == null)
            {
                return NotFound();
            }

            var assetlocation =  await _context.AssetLocations.FirstOrDefaultAsync(m => m.Id == id);
            if (assetlocation == null)
            {
                return NotFound();
            }
            AssetLocation = assetlocation;
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

            _context.Attach(AssetLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetLocationExists(AssetLocation.Id))
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

        private bool AssetLocationExists(int id)
        {
          return (_context.AssetLocations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
