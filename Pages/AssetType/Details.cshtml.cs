using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetTypes
{
    public class DetailsModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public DetailsModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

      public Types AssetType { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AssetTypes == null)
            {
                return NotFound();
            }

            var assettype = await _context.AssetTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (assettype == null)
            {
                return NotFound();
            }
            else 
            {
                AssetType = assettype;
            }
            return Page();
        }
    }
}
