using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetVendor
{
    public class IndexModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public IndexModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

        public IList<Vendors> AssetVendor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AssetVendors != null)
            {
                AssetVendor = await _context.AssetVendors.ToListAsync();
            }
        }
    }
}
