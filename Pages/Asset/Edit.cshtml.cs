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

        [BindProperty]
        public AssetModel Asset { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
           ViewData["AssigneeId"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["CreateByUser"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["LocationId"] = new SelectList(_context.AssetLocations, "Id", "Id");
           ViewData["StatusId"] = new SelectList(_context.AssetStatuses, "Id", "Id");
           ViewData["TransactionId"] = new SelectList(_context.AssetTransactions, "Id", "Id");
           ViewData["TypeId"] = new SelectList(_context.AssetTypes, "Id", "Id");
           ViewData["VendorId"] = new SelectList(_context.AssetVendors, "Id", "Id");
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
