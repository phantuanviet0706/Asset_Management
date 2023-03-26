using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetTypes
{
    public class EditModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public EditModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }
        public Users userProfile { get; set; } = default!;
        public string Msg;

        [BindProperty]
        public Types AssetType { get; set; } = default!;

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

            if (id == null || _context.AssetTypes == null)
            {
                return NotFound();
            }

            var assettype =  await _context.AssetTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (assettype == null)
            {
                return NotFound();
            }
            AssetType = assettype;
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

            if (AssetType.Name == null)
            {
                Msg = "Location Name can't be empty";
                ViewData["msg"] = Msg;
                return Page();
            }

            if (AssetType.Code == null)
            {
                var code = AssetType.Name.Substring(0, 1).ToUpper();
                AssetType.Code = code + code;
            }

            _context.Attach(AssetType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetTypeExists(AssetType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Setting/Types");
        }

        private bool AssetTypeExists(int id)
        {
          return (_context.AssetTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
