using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetVendor
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
            return Page();
        }

        [BindProperty]
        public Vendors AssetVendor { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.AssetVendors == null || AssetVendor == null)
            {
                return Page();
            }

            _context.AssetVendors.Add(AssetVendor);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Setting/Vendors");
        }
    }
}
