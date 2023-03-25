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
    public class IndexModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public IndexModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

        public IList<Types> AssetType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AssetTypes != null)
            {
                AssetType = await _context.AssetTypes.ToListAsync();
            }
        }
    }
}
