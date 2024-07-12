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
using PetSpaService.BookingService;

namespace PRN211GroupProject.Pages.Admin.BookingPage
{
    public class DeleteModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public DeleteModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [BindProperty]
      public Booking Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
            {
                return Unauthorized();
            }
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null || _bookingService.GetBookingList() == null)
            {
                return NotFound();
            }
            _bookingService.DeleteBooking(id);

            return RedirectToPage("./Index");
        }
    }
}
