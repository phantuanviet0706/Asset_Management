﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetVendor
{
    public class DetailsModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public DetailsModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

      public Vendors AssetVendor { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AssetVendors == null)
            {
                return NotFound();
            }

            var assetvendor = await _context.AssetVendors.FirstOrDefaultAsync(m => m.Id == id);
            if (assetvendor == null)
            {
                return NotFound();
            }
            else 
            {
                AssetVendor = assetvendor;
            }
            return Page();
        }
    }
}
