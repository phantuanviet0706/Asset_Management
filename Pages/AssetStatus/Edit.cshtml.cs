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
    public class EditModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public EditModel(Project_PRN.Models.ProjectPrn221Context context)
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


        [BindProperty]
        public Statuses AssetStatus { get; set; } = default!;
        public string Msg;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string username = HttpContext.Session.GetString("username");
            if (username == null || _context.Users == null)
            {
                return RedirectToPage("/Login");
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return RedirectToPage("/Login");
            }
            if (user.UserCode.ToLower() != "admin")
            {
                return RedirectToPage("/Login");
            }
            userProfile = user;
            if (id == null || _context.AssetStatuses == null)
            {
                return NotFound();
            }

            var assetstatus =  await _context.AssetStatuses.FirstOrDefaultAsync(m => m.Id == id);
            if (assetstatus == null)
            {
                return NotFound();
            }
            AssetStatus = assetstatus;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            string username = HttpContext.Session.GetString("username");
            if (username == null || _context.Users == null)
            {
                return RedirectToPage("/Login");
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return RedirectToPage("/Login");
            }
            if (user.UserCode.ToLower() != "admin")
            {
                return RedirectToPage("/Login");
            }
            userProfile = user;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            AssetStatus.Active = active ? 1 : 0;
            AssetStatus.AvailableToUse = availableToUse ? 1 : 0;
            AssetStatus.CurrentlyInUse = currentlyInUse ? 1 : 0;

            _context.Attach(AssetStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetStatusExists(AssetStatus.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Setting/Statuses");
        }

        private bool AssetStatusExists(int id)
        {
          return (_context.AssetStatuses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
