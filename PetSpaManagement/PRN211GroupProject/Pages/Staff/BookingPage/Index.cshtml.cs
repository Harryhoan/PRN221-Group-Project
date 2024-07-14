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

namespace PRN211GroupProject.Pages.Staff.BookingPage
{
    public class IndexModel : PageModel
    {
        private readonly IBookingService _booking;

        public IndexModel(IBookingService booking)
        {
            _booking = booking;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Staff")
            {
                return Unauthorized();
            }
            if (_booking.GetBookingList() != null)
            {
                Booking = _booking.GetBookingList();
            }
            return Page();
        }
    }
}
