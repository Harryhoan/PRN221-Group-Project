using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;
using PetSpaService.BookingService;

namespace PRN211GroupProject.Pages.Admin.BookingPage
{
    public class DetailsModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IAccountService _accountService;

        public DetailsModel(IBookingService bookingService, IAccountService accountService)
        {
            _bookingService = bookingService;
            _accountService = accountService;

        }

      public Booking Booking { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _bookingService.GetBookingList() == null)
            {
                return NotFound();
            }

            var booking = _bookingService.GetBooking(id);
            if (booking == null)
            {
                return NotFound();
            }
            else 
            {
                Booking = booking;
            }
            ViewData["account"] = new SelectList(_accountService.GetAllAccount(), "Id", "email");
            return Page();
        }
    }
}
