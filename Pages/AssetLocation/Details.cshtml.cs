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
    public class DetailsModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public DetailsModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

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
    }
}
