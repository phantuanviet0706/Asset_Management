using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project_PRN.Pages
{
    public class ForgotModel : PageModel
    {
        private readonly ILogger<ForgotModel> _logger;

        public ForgotModel(ILogger<ForgotModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
