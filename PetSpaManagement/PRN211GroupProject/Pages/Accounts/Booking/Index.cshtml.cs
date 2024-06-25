using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaService.AccountService;
using PetSpaService.BookingService;
using System.Security.Claims;

namespace PRN211GroupProject.Pages.Accounts
{
    public class BookingModel : PageModel
    {

        private readonly IBookingService _bookingService;
        private readonly IAccountService _accountService;

        public BookingModel(IBookingService bookingService, IAccountService accountService)
        {
            _bookingService = bookingService;
            _accountService = accountService;
        }

        public IList<PetSpaBussinessObject.Booking>? Bookings { get; set; }
        public IActionResult OnGet()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Email);
                if (User.Identity != null && userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
                {
                    var account = _accountService.GetAccountByEmail(userIdClaim.Value);
                    if (account != null)
                    {
                        Bookings = (IList<PetSpaBussinessObject.Booking>?)_bookingService.GetWeeklyBooking(DateTime.Now, _bookingService.GetAccountBookingList(account.Id));
                    }
                }
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }

        }

        public IActionResult OnPost()
        {
            {
                try
                {
                    if (User.Identity != null)
                    {
                        var userIdClaim = User.FindFirst(ClaimTypes.Email);
                        if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
                        {
                            var account = _accountService.GetAccountByEmail(userIdClaim.Value);
                            if (account != null)
                            {
                                Bookings = (IList<PetSpaBussinessObject.Booking>?)_bookingService.GetWeeklyBooking(DateTime.Now, _bookingService.GetAccountBookingList(account.Id));
                                return Page();
                            }
                            else 
                            { 
                                return NotFound(); 
                            }
                        }
                    }
                    return Unauthorized();
                }
                catch
                {
                    return BadRequest();
                }

            }
        }
    }
}
