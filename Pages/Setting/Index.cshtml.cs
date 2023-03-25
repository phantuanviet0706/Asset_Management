using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Project_PRN.Pages.Setting
{
    public class IndexModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public IndexModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }
    }
}
