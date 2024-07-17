using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;
using PetSpaService.RolesService;
using PetSpaService.VoucherService.VoucherService;

namespace PRN211GroupProject.Pages.AccountPage
{
    public class EditModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IVoucherService _voucher;
        private readonly IRoleService _role;
        public EditModel(IAccountService account, IVoucherService voucher, IRoleService role)
        {
            accountService = account;
            _voucher = voucher;
            _role = role;
            Account = new();
        }

        [BindProperty]
        public Account Account { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
				var roleClaim = User.FindFirst(ClaimTypes.Role);
				if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
				{
					return Unauthorized();
				}
				if (id == null || id <= 0 || accountService.GetAllAccount() == null)
				{
                    return NotFound();
				}
				var account = accountService.GetAccount((int)id);
				if (account == null || account.Id <= 0)
				{
					return NotFound();
				}

				// Fetch and filter vouchers
				var vouchers = _voucher.GetVoucherList();
				var activeVouchers = vouchers.Where(v => v.Status == true).ToList();
				ViewData["voucherlist"] = new SelectList(activeVouchers, "Id", "Name");

				ViewData["rolelist"] = new SelectList(_role.GetAllRole(), "Id", "Name");
				Account = account;
				return Page();
			}
            catch
            {
                return BadRequest();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (accountService == null || Account == null || Account.Id <= 0)
                {
                    return BadRequest();
                }
                if (String.IsNullOrEmpty(Account.Name))
                {
                    return BadRequest();
                }
                if (String.IsNullOrEmpty(Account.Email))
                {
                    return BadRequest();
                }
                if (Account.CountVoucher < 0)
                {
                    return BadRequest();
                }
                accountService.UpdateAccount(Account);
				return RedirectToPage();
			}
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}