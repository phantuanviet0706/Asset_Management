using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_PRN.Models;

namespace Project_PRN.Pages.Asset
{
    public class CreateModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public CreateModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AssigneeId"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["CreateByUser"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["LocationId"] = new SelectList(_context.AssetLocations, "Id", "Id");
        ViewData["StatusId"] = new SelectList(_context.AssetStatuses, "Id", "Id");
        ViewData["TransactionId"] = new SelectList(_context.AssetTransactions, "Id", "Id");
        ViewData["TypeId"] = new SelectList(_context.AssetTypes, "Id", "Id");
        ViewData["VendorId"] = new SelectList(_context.AssetVendors, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public AssetModel Asset { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Assets == null || Asset == null)
            {
                return Page();
            }

            _context.Assets.Add(Asset);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
