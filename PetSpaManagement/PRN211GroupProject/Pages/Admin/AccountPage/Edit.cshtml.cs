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
        private readonly IAccountService _account;
        private readonly IVoucherService _voucher;
        private readonly IRoleService _role;
        public EditModel(IAccountService account, IVoucherService voucher, IRoleService role)
        {
            _account = account;
            _voucher = voucher;
            _role = role;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
            {
                return Unauthorized();
            }
            if (id == null || _account.GetAllAccount() == null)
            {
                return NotFound();
            }
            var account = _account.GetAccount(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["voucherlist"] = new SelectList(_voucher.GetVoucherList(), "Id", "Name");
            ViewData["rolelist"] = new SelectList(_role.GetAllRole(), "Id", "Name");
            Account = account;
            return Page();
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _account.UpdateAccount(Account);
            }
            catch (Exception ex)
            {
            }

            return RedirectToPage("./Index");
        }
    }
}