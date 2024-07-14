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
using PetSpaService.AvailableService;
using PetSpaService.BookingService;

namespace PRN211GroupProject.Pages.Staff.BookingPage
{
    public class DetailsModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IAccountService _accountService;
        private readonly IAvailableService _availableService;

        public DetailsModel(IBookingService bookingService, IAccountService accountService, IAvailableService availableService)
        {
            _bookingService = bookingService;
            _accountService = accountService;
            _availableService = availableService;

        }

      public Booking Booking { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Staff")
            {
                return Unauthorized();
            }
            var account = _accountService.GetAllAccount();
            var available = _availableService.GetAvailableList();
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
            ViewData["account"] = account.Select(acc => new SelectListItem
            {
                Value = acc.Id.ToString(),
                Text = acc.Email
            }).ToList();
            ViewData["available"] = new SelectList(_availableService.GetAvailableList(), "Id", "Name");
            return Page();
        }
    }
}
