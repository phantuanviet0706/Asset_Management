using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PRN.Models;

namespace Project_PRN.Pages.Asset
{
    public class ReturnModel : PageModel
    {
        private readonly Project_PRN.Models.ProjectPrn221Context _context;

        public ReturnModel(Project_PRN.Models.ProjectPrn221Context context)
        {
            _context = context;
        }
        public Users userProfile { get; set; } = default!;
        public string Msg;

        [BindProperty]
        public Assets Asset { get; set; } = default!;
        public IList<Users> usersAssign { get; set; }

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

            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FirstOrDefaultAsync(m => m.Id == id);

            if (asset == null)
            {
                return NotFound();
            }
            else
            {
                if (_context.AssetLocations != null)
                {
                    asset.Location = await _context.AssetLocations.FirstOrDefaultAsync(m => m.Id == asset.LocationId);
                }

                if (_context.AssetStatuses != null)
                {
                    asset.Status = await _context.AssetStatuses.FirstOrDefaultAsync(m => m.Id == asset.StatusId);
                }

                if (_context.AssetTypes != null)
                {
                    asset.Type = await _context.AssetTypes.FirstOrDefaultAsync(m => m.Id == asset.TypeId);
                }

                if (_context.AssetVendors != null)
                {
                    asset.Vendor = await _context.AssetVendors.FirstOrDefaultAsync(m => m.Id == asset.VendorId);
                }

                if (_context.Users != null)
                {
                    asset.Assignee = await _context.Users.FirstOrDefaultAsync(m => m.Id == asset.AssigneeId);
                }

                if (_context.Users != null)
                {
                    ViewData["UserAssign"] = new SelectList(_context.Users, "Id", "Username");
                }
                Asset = asset;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
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

            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            if (_context.AssetLocations != null)
            {
                asset.Location = await _context.AssetLocations.FirstOrDefaultAsync(m => m.Id == asset.LocationId);
            }

            if (_context.AssetStatuses != null)
            {
                asset.Status = await _context.AssetStatuses.FirstOrDefaultAsync(m => m.Id == asset.StatusId);
            }

            if (_context.AssetTypes != null)
            {
                asset.Type = await _context.AssetTypes.FirstOrDefaultAsync(m => m.Id == asset.TypeId);
            }

            if (_context.AssetVendors != null)
            {
                asset.Vendor = await _context.AssetVendors.FirstOrDefaultAsync(m => m.Id == asset.VendorId);
            }

            if (_context.Users != null)
            {
                asset.Assignee = await _context.Users.FirstOrDefaultAsync(m => m.Id == asset.AssigneeId);
            }

            Asset = asset;

            var employee = _context.Users.FirstOrDefault(u => u.Id == asset.AssigneeId);
            if (employee == null)
            {
                Msg = "Cannot find Employee";
                ViewData["msg"] = Msg;
                return Page();

            }
            asset.AssigneeId = null;
            asset.StatusId = 4;
            _context.Attach(asset).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var transaction = initTransaction(asset, employee);
            if (transaction == null)
            {
                Msg = "Cannot init Transaction";
                ViewData["msg"] = Msg;
                return Page();
            }

            return RedirectToPage("./Index");
        }

        public Transactions initTransaction(Assets asset, Users employee)
        {
            DateTimeOffset currentTime = new DateTimeOffset(DateTime.UtcNow);
            var transaction = new Transactions();
            transaction.Name = "Return Asset " + asset.Name + " from " + employee.Username;
            transaction.AssetId = asset.Id;
            transaction.TransactionDate = asset.AcquisitionDate;
            if (asset.AcquisitionDate == null)
            {
                transaction.TransactionDate = DateTime.Now;
            }
            transaction.TransactionType = "asset";
            transaction.TransactionCost = 0;
            transaction.CreatedAt = (int)currentTime.ToUnixTimeSeconds();

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
