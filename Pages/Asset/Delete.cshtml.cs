using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.Asset
{
    public class DeleteModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public DeleteModel(Project_PRN.Models.ProjectPrn221Context context)
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

            var asset = await _context.Assets.FirstOrDefaultAsync(m => m.Id == id);

            if (asset == null)
            {
                return NotFound();
            }
            else 
            {
                Asset = asset;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }
            var asset = await _context.Assets.FindAsync(id);

            if (asset != null)
            {
                Asset = asset;
                _context.Assets.Remove(Asset);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
