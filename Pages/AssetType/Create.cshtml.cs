using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetTypes
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
        public Types AssetType { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.AssetTypes == null || AssetType == null)
            {
                return Page();
            }

            _context.AssetTypes.Add(AssetType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
