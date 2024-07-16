using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;
using PetSpaService.VoucherService.VoucherService;
using PRN211GroupProject.Utilities;

namespace PRN211GroupProject.Pages.Admin.VoucherPage
{
    public class IndexModel : PageModel
    {
        private readonly IVoucherService _voucherService;
        private readonly IAccountService _accountService;

        public IndexModel(IVoucherService voucherService, IAccountService account)
        {
            _voucherService = voucherService;
            _accountService = account;
        }
        [TempData]
        public string errorMessage { get; set; }

        public IList<Voucher> Voucher { get;set; } = default!;
        [BindProperty]
        public Voucher NewVoucher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var Account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
            if (Account == null)
            {
                errorMessage = "You must login first";
                return RedirectToPage("/Accounts/Login");
            }
            if (_voucherService != null)
            {
                Voucher = _voucherService.GetVoucherList();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (NewVoucher != null)
            {
                _voucherService.AddVoucher(NewVoucher);
            }
            return RedirectToPage();
        }
    }
}
