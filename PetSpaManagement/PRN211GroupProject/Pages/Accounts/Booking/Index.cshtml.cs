using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PetSpaService.BookingService;
using System.Security.Claims;

namespace PRN211GroupProject.Pages.Accounts
{
    public class BookingModel : PageModel
    {

        private readonly IBookingService _bookingService;
        private readonly IAccountService _accountService;
        public int _offset { get; set; } = 0;

        public BookingModel(IBookingService bookingService, IAccountService accountService)
        {
            _bookingService = bookingService;
            _accountService = accountService;
        }

        public IList<PetSpaBussinessObject.Booking>? Bookings { get; set; }
        /*public IActionResult OnGet()
        {
            try
            {

                if (User.Identity != null && !String.IsNullOrEmpty(User.Identity.Name) && User.Identity.IsAuthenticated)
                {
                    //var userIdClaim = User.FindFirst(ClaimTypes.Email);
                    //if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
                    //{
                    //var account = _accountService.GetAccountByEmail(userIdClaim.Value);
                    //if (account != null)
                    //{
                    //    Bookings = _bookingService.GetWeeklyBooking(DateTime.Now, _bookingService.GetAccountBookingList(account.Id));
                    //    return Page();
                    //}
                    //else 
                    //    return NotFound();

                    //}
                    Bookings = _bookingService.GetWeeklyBooking(DateTime.Now, _bookingService.GetActiveBookingList());
                    return Page();
                }
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }

        }*/
        public IActionResult OnGet(int? offset) 
        {
            try
            {
                if (User.Identity != null && !String.IsNullOrEmpty(User.Identity.Name) && User.Identity.IsAuthenticated)
                {
                    if (offset != null && offset != 0) 
                    {
                        _offset = (int)offset;
                        Bookings = _bookingService.GetWeeklyBooking(DateTime.Now.Date.AddDays((Double)offset * 7), _bookingService.GetActiveBookingList()); 
                    }
                       
                    else
                        Bookings = _bookingService.GetWeeklyBooking(DateTime.Now.Date, _bookingService.GetActiveBookingList());
                    return Page();
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
            //{
            //    try
            //    {
            //        if (User.Identity != null && !String.IsNullOrEmpty(User.Identity.Name) && User.Identity.IsAuthenticated)
            //        {
            //            if (Request != null)
            //            {
            //                var week = Request.Form["Week"];
                            
            //                    Bookings = _bookingService.GetWeeklyBooking(DateTime.Now, _bookingService.GetActiveBookingList());
            //                    return Page();
                            
            //            }
            //        }
            //        return Unauthorized();
            //    }
            //    catch
            //    {
            //        return RedirectToPage("Login");
            //    }

            //}
            return Page();
        }
    }
}
