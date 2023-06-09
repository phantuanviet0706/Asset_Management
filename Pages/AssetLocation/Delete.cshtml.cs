﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetLocation
{
    public class DeleteModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public DeleteModel(Project_PRN.Models.ProjectPrn221Context context)
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

            var assetlocation = await _context.AssetLocations.FirstOrDefaultAsync(m => m.Id == id);

            if (assetlocation == null)
            {
                return NotFound();
            }
            else 
            {
                AssetLocation = assetlocation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.AssetLocations == null)
            {
                return NotFound();
            }
            var assetlocation = await _context.AssetLocations.FindAsync(id);

            if (assetlocation != null)
            {
                AssetLocation = assetlocation;
                _context.AssetLocations.Remove(AssetLocation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
