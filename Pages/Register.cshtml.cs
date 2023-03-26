using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Project_PRN.Models;
using System.Text.RegularExpressions;

namespace Project_PRN.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        public string Email { get; set; }

        public ProjectPrn221Context context { get; set; }
        public RegisterModel(ILogger<RegisterModel> logger, ProjectPrn221Context _context)
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
            Users user = new Users();
            Users user_by_username = context.Users.FirstOrDefault(u => u.Username == Username);
            if (Username.IsNullOrEmpty())
            {
                Msg = "Username can't be empty";
                ViewData["Msg"] = Msg;
                return Page();
            }
            if (user_by_username != null)
            {
                Msg = "Username has already been taken! Please choose another username";
                ViewData["Msg"] = Msg;
                return Page();
            }

            if (Email.IsNullOrEmpty())
            {
                Msg = "Email can't be empty";
                ViewData["Msg"] = Msg;
                return Page();
            }

            var isMatch = Regex.Matches(Email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            if (isMatch.Count == 0)
            {
                Msg = "Invalid Email";
                ViewData["Msg"] = Msg;
                return Page();
            }

            if (Password.IsNullOrEmpty())
            {
                Msg = "Password can't be empty";
                ViewData["Msg"] = Msg;
                return Page();
            }

            if (!ConfirmPassword.Equals(Password))
            {
                Msg = "Confirm Password must equal to Password";
                ViewData["Msg"] = Msg;
                return Page();
            }

            user.Username = Username;
            user.Password = Password;
            user.Email = Email;
            user.UserCode = "User";

            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            } 
            catch (Exception ex)
            {
                Msg = ex.Message;
                ViewData["Msg"] = Msg;
                return Page();
            }


            return RedirectToPage("/Login");
        }
    }
}
