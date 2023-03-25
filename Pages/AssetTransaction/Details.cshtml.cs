using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetTransaction
{
    public class DetailsModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public DetailsModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

      public Transactions AssetTransaction { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AssetTransactions == null)
            {
                return NotFound();
            }

            var assettransaction = await _context.AssetTransactions.FirstOrDefaultAsync(m => m.Id == id);
            if (assettransaction == null)
            {
                return NotFound();
            }
            else 
            {
                AssetTransaction = assettransaction;
            }
            return Page();
        }
    }
}
