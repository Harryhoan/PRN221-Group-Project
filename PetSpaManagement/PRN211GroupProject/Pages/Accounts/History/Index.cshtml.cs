using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaRepo.VoucherRepository;
using PetSpaService.AccountService;
using PetSpaService.BillService;
using PetSpaService.VoucherService.VoucherService;

namespace PRN211GroupProject.Pages.Accounts.History
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IBillService billService;
        private readonly IVoucherService voucherService;
        public IndexModel(IAccountService account, IBillService bill, IVoucherService voucher)
        {
            accountService = account;
            billService = bill;
            voucherService = voucher;
        }
        public Account? Account { get; set; }
        public List<Bill> Bill { get; set; } = default!;
        public IActionResult OnGetAsync()
        {
            SetAccount();
            if (Account != null)
            {
                if (billService != null)
                {
                    Bill = billService.GetAccountBillList(Account.Id);
                }
                return Page();
            }   
            else
            {
                return NotFound();
            }
        }
        public void SetAccount()
        {
            try
            {
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                var emailClaim = claimsIdentity?.FindFirst(ClaimTypes.Email);
                if (emailClaim == null)
                {
                    Account = null;
                    return;
                }
                Account = accountService.GetAccountByEmail(emailClaim.Value);
            }
            catch
            {
                Account = null;
            }
        }

    }
}
