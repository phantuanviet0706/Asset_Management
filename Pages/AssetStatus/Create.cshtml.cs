using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetStatus
{
    public class CreateModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public CreateModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }
        public Users userProfile { get; set; } = default!;

        [BindProperty]
        public bool availableToUse { get; set; }
        [BindProperty]
        public bool active { get; set; }
        [BindProperty]
        public bool currentlyInUse { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string username = HttpContext.Session.GetString("username");
            if (username == null || _context.Users == null)
            {
                //return RedirectToPage("/Login");
                username = "admin";
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return RedirectToPage("/Login");
            }
            userProfile = user;
            return Page();
        }

        [BindProperty]
        public Statuses AssetStatus { get; set; } = default!;
        public string Msg;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string username = HttpContext.Session.GetString("username");
            if (username == null || _context.Users == null)
            {
                //return RedirectToPage("/Login");
                username = "admin";
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return RedirectToPage("/Login");
            }
            userProfile = user;

            if (!ModelState.IsValid || _context.AssetStatuses == null || AssetStatus == null)
            {
                Msg = "Location Name can't be empty";
                ViewData["msg"] = Msg;
                return Page();
            }

            if (AssetStatus.Name == null)
            {
                Msg = "Status Name can't be empty";
                ViewData["msg"] = Msg;
                return Page();
            }

            AssetStatus.Active = active ? 1 : 0;
            AssetStatus.AvailableToUse = availableToUse ? 1 : 0;
            AssetStatus.CurrentlyInUse = currentlyInUse ? 1 : 0;

            _context.AssetStatuses.Add(AssetStatus);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Setting/Statuses");
        }
    }
}
