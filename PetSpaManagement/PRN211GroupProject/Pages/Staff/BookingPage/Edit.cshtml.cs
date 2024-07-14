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
using PetSpaService.SpotService.SpotService;

namespace PRN211GroupProject.Pages.Staff.BookingPage
{
    public class EditModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IAccountService _accountService;
        private readonly IAvailableService _availableService;
        private readonly ISpotService _spotService;

        public EditModel(IBookingService bookingService, IAccountService accountService, IAvailableService availableService, ISpotService spotService)
        {
            _bookingService = bookingService;
            _accountService = accountService;
            _availableService = availableService;
            _spotService = spotService;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;
        public int SpotId { get; set; } = -1;
        public async Task<IActionResult> OnGetAsync(int id, int? spotId)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
            {
                return Unauthorized();
            }
            var spotList = _spotService.GetActiveSpotList();

            if (spotId == null || spotId < 1)
            {
                spotId = spotList[0].Id;
            }
            SpotId = (int)spotId;

            ViewData["availableList"] = _availableService.GetAvailableListBySpot((int)spotId).Select(available => new SelectListItem
            {
                Value = available.Id.ToString(),
                Text = available.Service.Name + " - " + available.Spot.Name
            }).ToList();
            ViewData["AccountId"] = new SelectList(_accountService.GetAllAccount(), "Id", "Name");
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
            try
            {
                Booking.Status = true;
                _bookingService.UpdateBooking(Booking);
            }
            catch (Exception ex)
            {
            }

            return RedirectToPage("./Index");
        }
    }
}
