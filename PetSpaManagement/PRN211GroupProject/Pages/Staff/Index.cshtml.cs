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

namespace PRN211GroupProject.Pages.Staff
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
        public double[] monthlySum { get; set; } = new double[12];
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
                if (Account == null)
                {
                    return RedirectToPage("/Error");
                }

                if (_billService == null || _accountService == null)
                {
                    return BadRequest();
                }

                Accounts = _accountService.GetAllAccountCreatedThisYear();

                Bill = _billService.GetBillCreatedThisYearList();
                if (Bill != null && Bill.Count > 0)
                {
                    foreach (var item in Bill)
                    {
                        monthlySum[item.Created.Month - 1] += item.Total;
                    }
                }

                UserCount = _accountService.NumberOfUser();
                bookingCount = _bookingService.NumberOfBooking();
                serviceCount = _serviceService.NumberOfService();
                FeedbackCount = _feedbackService.NumberOfFeedback();
                return Page();
            }
            catch
            {
                errorMessage = "Something went wrong. Please try again.";
                return RedirectToPage("/Error");
            }
        }
    }
}
