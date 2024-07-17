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

namespace PRN211GroupProject.Pages.Staff.VoucherPage
{
    public class IndexModel : PageModel
    {
        private readonly IVoucherService _voucherService;
        private readonly IAccountService accountService;
        public IndexModel(IVoucherService voucherService, IAccountService account)
        {
            _voucherService = voucherService;
            accountService = account;
        }
        [TempData]
        public string errorMessage { get; set; }
        public IList<Voucher> Voucher { get;set; } = default!;
        [BindProperty]
        public Voucher NewVoucher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
            if (Account == null)
            {
                return Unauthorized();
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
                NewVoucher.Status = false;
                _voucherService.AddVoucher(NewVoucher);
            }
            return RedirectToPage();
        }
    }
}
