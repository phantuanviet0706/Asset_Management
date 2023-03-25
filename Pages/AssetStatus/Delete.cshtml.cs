﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetStatus
{
    public class DeleteModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public DeleteModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Statuses AssetStatus { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AssetStatuses == null)
            {
                return NotFound();
            }

            var assetstatus = await _context.AssetStatuses.FirstOrDefaultAsync(m => m.Id == id);

            if (assetstatus == null)
            {
                return NotFound();
            }
            else 
            {
                AssetStatus = assetstatus;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.AssetStatuses == null)
            {
                return NotFound();
            }
            var assetstatus = await _context.AssetStatuses.FindAsync(id);

            if (assetstatus != null)
            {
                AssetStatus = assetstatus;
                _context.AssetStatuses.Remove(AssetStatus);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
