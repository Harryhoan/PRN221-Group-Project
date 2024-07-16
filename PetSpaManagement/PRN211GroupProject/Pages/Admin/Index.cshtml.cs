using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.AvailableService;
using PetSpaService.BillService;
using PetSpaService.BookingService;
using PetSpaService.FeedbacksService;
using PRN211GroupProject.Utilities;
using System.Security.Claims;

namespace PRN211GroupProject.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IBillService _billService;
        private readonly IAccountService _accountService;
        private readonly IAvailableService _availableService;
        private readonly IBookingService _bookingService;
        private readonly IServiceService _serviceService;
        private readonly IFeedbackService _feedbackService;

        public IndexModel(IBillService billService, IAccountService accountService, IAvailableService availableService, IBookingService bookingService, IServiceService serviceService, IFeedbackService feedbackService)
        {
            _billService = billService;
            _accountService = accountService;
            _availableService = availableService;
            _bookingService = bookingService;
            _serviceService = serviceService;
            _feedbackService = feedbackService;
        }
        public Account? Account { get; set; }
        [TempData]
        public string errorMessage { get; set; }

        public IList<Bill> Bill { get; set; } = default!;
        public IList<Account> Accounts { get; set; } = default!;
        public int UserCount { get; set; }
        public int bookingCount { get; set; }
        public int serviceCount { get; set; }
        public int FeedbackCount { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
            if (Account != null)
            {
                try
                {
                    if (_billService == null)
                    {
                        return BadRequest();
                    }
                    Bill = _billService.GetBillList();
                    if (_accountService != null)
                    {
                        Accounts = _accountService.GetAllAccount();
                        UserCount = _accountService.NumberOfUser();
                        bookingCount = _bookingService.NumberOfBooking();
                        serviceCount = _serviceService.NumberOfService();
                        FeedbackCount = _feedbackService.NumberOfFeedback();
                    }
                }
                catch
                {
                    return BadRequest();
                }
                return Page();
            }
            else
            {
                errorMessage = "You must login first";
                return RedirectToPage("/Accounts/Login");
            }
        }
    }
}
