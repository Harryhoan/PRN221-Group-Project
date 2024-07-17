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
using PRN211GroupProject.Utilities;

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
            Booking = new();
        }

        [BindProperty]
        public Booking Booking { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
				if (HttpContext == null || HttpContext.Session == null)
				{
					return BadRequest();
				}
				if (_availableService == null || _accountService == null || _bookingService == null || _spotService == null)
				{
					return BadRequest();
				}

				if (AccountUtilities.Instance.GetAccount(HttpContext, _accountService) == null)
				{
					return RedirectToPage("/Accounts/Login");
				}

				if (id == null || id <= 0)
				{
					return BadRequest();
				}

				Booking = _bookingService.GetBooking((int)id);

				if (Booking == null || Booking.Id <= 0 || Booking.AvailableId <= 0)
				{
					return NotFound();
				}

				var available = _availableService.GetAvailable(Booking.AvailableId);

				if (available == null || available.Id <= 0 || available.SpotId <= 0 || available.Service == null)
				{
					return NotFound();
				}


				ViewData["availableList"] = _availableService.GetAvailableListBySpot(available.SpotId).Select(available => new SelectListItem
				{
					Value = available.Id.ToString(),
					Text = available.Service.Name + " - " + available.Spot.Name
				}).ToList();

				return Page();
			}
            catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
				if (HttpContext == null || HttpContext.Session == null)
				{
					return BadRequest();
				}

                var currentUser = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
                if (currentUser == null)
                {
                    return RedirectToPage("/Accounts/Login");
                }

                if (currentUser.RoleId == 1 || currentUser.RoleId == 2)
                {
                    return Unauthorized();
                }

                if (Booking == null || Booking.Started == default || Booking.Started.Date <= DateTime.Today || Booking.Started.TimeOfDay < TimeSpan.FromHours(9) || Booking.Started.TimeOfDay > TimeSpan.FromHours(18))
                {
                    return BadRequest();
                }
                var available = _availableService.GetAvailable(Booking.AvailableId);
                if (available == null || available.Service == null || available.Spot == null)
                {
                    return BadRequest();
                }
                Booking.Ended = Booking.Started.AddMinutes(available.Service.Duration);
                if (Booking.Ended.TimeOfDay < TimeSpan.FromHours(9) || Booking.Ended.TimeOfDay > TimeSpan.FromHours(18) || _bookingService.IsActiveBookingConflictBySpot(Booking.Started, Booking.Ended, Booking.Available.SpotId))
                {
                    return BadRequest();
                }
                _bookingService.UpdateBooking(Booking);
            }
            catch (Exception ex)
            {
            }

            return RedirectToPage("./Index");
        }
    }
}
