using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.AvailableService;
using PetSpaService.BillDetailedService;
using PetSpaService.BillService;
using PetSpaService.BookingService;
using PetSpaService.SpotService.SpotService;
using PetSpaService.VoucherService.VoucherService;

namespace PRN211GroupProject.Pages.Accounts.History
{
    public class IndexModel : PageModel
    {
        private readonly IBillDetailedService billDetailedService;
        private readonly IBillService billService;
        private readonly IAccountService accountService;
        private readonly IBookingService bookingService;
        private readonly IAvailableService availableService;
        private readonly IServiceService serviceService;
        private readonly ISpotService spotService;
        private readonly IVoucherService voucherService;

        public IndexModel(IBillDetailedService billDetailed, IAccountService account, IBillService bill, IBookingService booking, IAvailableService available, IServiceService service, ISpotService spot, IVoucherService voucher)
        {
            billDetailedService = billDetailed;
            billService = bill;
            accountService = account;
            bookingService = booking;
            availableService = available;
            serviceService = service;
            spotService = spot;
            voucherService = voucher;
        }

        public Account? Account { get; set; }
        public List<Bill> Bill { get; set; } = new List<Bill>();

        public IActionResult OnGet()
        {
            SetAccount();

            if (Account != null)
            {
                // Fetch list of bills associated with the account
                Bill = billService.GetAccountBillList(Account.Id);

                if (Bill == null || Bill.Count == 0)
                {
                    return NotFound("No bills found for the account.");
                }

                return Page();
            }
            else
            {
                return NotFound("Account not found.");
            }
        }

        public string errorMessage { get; set; }

        public IActionResult OnPostSortDate(DateTime fromDate, DateTime toDate)
        {
            SetAccount();
            if (Account != null)
            {
                if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
                {
                    Bill = billService.GetFilterdAccountBill(fromDate, toDate, Account.Id);
                    if (Bill.Count > 0)
                    {
                        return Page();
                    }
                    else
                    {
                        errorMessage = $"No Order's from {fromDate} to {toDate}";
                        return Page();
                    }
                }
                else
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
