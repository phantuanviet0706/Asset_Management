﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.AssetLocation
{
    public class CreateModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public CreateModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

        public Users userProfile { get; set; } = default!;

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
        public Locations AssetLocation { get; set; } = default!;
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
            if (!ModelState.IsValid || _context.AssetLocations == null || AssetLocation == null)
            {
                Msg = "Location Name can't be empty";
                ViewData["msg"] = Msg;
                return Page();
            }

          if (AssetLocation.Name == null)
            {
                Msg = "Location Name can't be empty";
                ViewData["msg"] = Msg;
                return Page();
            }

            _context.AssetLocations.Add(AssetLocation);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Setting/Index");
        }
    }
}
