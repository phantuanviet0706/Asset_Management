using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.Asset
{
    public class CreateModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public CreateModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
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

            ViewData["LocationId"] = new SelectList(_context.AssetLocations, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.AssetStatuses, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.AssetTypes, "Id", "Name");
            ViewData["VendorId"] = new SelectList(_context.AssetVendors, "Id", "Name");
            return Page();
        }
        public Users userProfile { get; set; } = default!;

        [BindProperty]
        public Assets Asset { get; set; } = default!;

        public string Msg;

        private const int STATUS_AVAILABLE = 4;
        private const string TYPE_ASSET = "asset";

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["LocationId"] = new SelectList(_context.AssetLocations, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.AssetStatuses, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.AssetTypes, "Id", "Name");
            ViewData["VendorId"] = new SelectList(_context.AssetVendors, "Id", "Name");
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

            if (!ModelState.IsValid || _context.Assets == null || Asset == null)
            {
                return Page();
            }

            if (Asset.Name == null || Asset.Name.ToString().Trim() == "")
            {
                Msg = "Please input Asset Name";
                ViewData["msg"] = Msg;
                return Page();
            }

            if (Asset.LocationId == null)
            {
                Msg = "Please choose a Location";
                ViewData["msg"] = Msg;
                return Page();
            }

            Asset.StatusId = STATUS_AVAILABLE;
            Asset.CreateByUser = userProfile.Id;

            if (Asset.AcquisitionDate == null)
            {
                Asset.AcquisitionDate = DateTime.Now;
            }

            _context.Assets.Add(Asset);
            await _context.SaveChangesAsync();

            var transaction = initTransaction(Asset);
            if (transaction == null)
            {
                Msg = "Cannot init Transaction";
                ViewData["msg"] = Msg;
                return Page();
            }

            return RedirectToPage("/Asset/Index");
        }
        public Transactions initTransaction(Assets asset)
        {
            DateTimeOffset currentTime = new DateTimeOffset(DateTime.UtcNow);
            var transaction = new Transactions();
            transaction.Name = "Create Asset " + asset.Name;
            transaction.AssetId = asset.Id;
            transaction.TransactionDate = asset.AcquisitionDate;
            if (asset.AcquisitionDate == null)
            {
                transaction.TransactionDate = DateTime.Now;
            }
            transaction.TransactionType = TYPE_ASSET;
            transaction.TransactionCost = 0;
            transaction.CreatedAt = (int) currentTime.ToUnixTimeSeconds();

            if (!ModelState.IsValid || _context.AssetTransactions == null || transaction == null)
            {
                return null;
            }

            _context.AssetTransactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }
    }

}
