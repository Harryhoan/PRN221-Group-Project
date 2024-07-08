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
    public class EditModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IAccountService _accountService;

        public EditModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [BindProperty]
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
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                _bookingService.UpdateBooking(Booking);
            }
            catch (Exception ex)
            {
                //if (!BookingExists(Booking.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return RedirectToPage("./Index");
        }

        //private bool BookingExists(int id)
        //{
        //    return (_context.Bookings?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
