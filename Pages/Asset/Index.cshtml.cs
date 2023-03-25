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
    public class IndexModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public IndexModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

        public IList<AssetModel> Asset { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Assets != null)
            {
                Asset = await _context.Assets
                .Include(a => a.Assignee)
                .Include(a => a.CreateByUserNavigation)
                .Include(a => a.Location)
                .Include(a => a.Status)
                .Include(a => a.Transaction)
                .Include(a => a.Type)
                .Include(a => a.Vendor).ToListAsync();
            }
        }
    }
}
