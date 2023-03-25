using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_PRN.Models;

namespace Project_PRN.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public ProjectPrn221Context context { get; set; }
        public IndexModel(ILogger<IndexModel> logger, ProjectPrn221Context _context)
        {
            _logger = logger;
            context = _context;
        }

        public string Msg;

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            Users user = context.Users.FirstOrDefault(u => u.Username == Username);
            if (user == null)
            {
                Msg = "Username not found";
                ViewData["Msg"] = Msg;
                return Page();
            }
            ViewData["username"] = Username;

            user = context.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);
            if (user == null)
            {
                Msg = "Invalid Password";
                ViewData["Msg"] = Msg;
                return Page();
            }
            HttpContext.Session.SetString("username", Username);
            return RedirectToPage("Asset/Index");
        }
    }
}