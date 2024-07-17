using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.AvailableService;
using PetSpaService.BookingService;
using PetSpaService.SpotService.SpotService;
using PRN211GroupProject.Utilities;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PRN211GroupProject.Pages.Accounts
{
    public class BookingModel : PageModel
    {

        private readonly IBookingService _bookingService;
        private readonly IAccountService _accountService;
        private readonly IAvailableService _availableService;
        private readonly IServiceService _serviceService;
        private readonly ISpotService _spotService;
        public int _offset { get; set; } = 0;

        public BookingModel(IBookingService bookingService, IAccountService accountService, IAvailableService availableService, IServiceService serviceService, ISpotService spotService)
        {
            _bookingService = bookingService;
            _accountService = accountService;
            _availableService = availableService;
            _serviceService = serviceService;
            _spotService = spotService;
        }

        [BindProperty]
        public PetSpaBussinessObject.Booking NewBooking { get; set; } = new PetSpaBussinessObject.Booking();
        public IList<PetSpaBussinessObject.Booking>? Bookings { get; set; }
        public int BookingCount { get; set; } = 0;

        [BindProperty]
        public int SpotId { get; set; } = -1;
        [TempData]
        public string errorMessage { get; set; }
		[TempData]
		public string successMessage { get; set; }

		public Account? Account { get; set; }
        public IActionResult OnGet(int? spotId, int? offset)
        {
            try
            {
                Account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
                if (Account != null)
                {
                    if (_availableService == null || _bookingService == null || _accountService == null || _serviceService == null)
                    {
                        return BadRequest();
                    }

                    var spotList = _spotService.GetActiveSpotList();
                    if (spotList == null || spotList.Count == 0)
                    {
                        return RedirectToPage("/Index");
                    }

                    if (spotId == null || spotId < 1)
                    {
                        spotId = spotList[0].Id;
                    }

                    SpotId = (int)spotId;

                    ViewData["spotList"] = spotList.Where(s => s.Status == true).Select(spot => new SelectListItem
                    {
                        Value = spot.Id.ToString(),
                        Text = spot.Name
                    }).ToList();

                    ViewData["availableList"] = _availableService.GetAvailableListBySpot((int)spotId).Where(a => a.Service.Status == true).Select(available => new SelectListItem
                    {
                        Value = available.Id.ToString(),
                        Text = available.Service.Name + " - " + available.Spot.Name
                    }).ToList();

                    if (offset != null && offset != 0)
                    {
                        _offset = (int)offset;
                        Bookings = _bookingService.GetWeeklyBooking(DateTime.Now.Date.AddDays((Double)offset * 7), _bookingService.GetActiveBookingListBySpot((int)spotId));
                    }

                    else
                        Bookings = _bookingService.GetWeeklyBooking(DateTime.Now.Date, _bookingService.GetActiveBookingListBySpot((int)spotId));
                    return Page();
                }
                else
                {
                    errorMessage = "You must login first";
                    return RedirectToPage("/Accounts/Login");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                Account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
                if (Account == null)
                {
                    errorMessage = "You must login first";
                    return RedirectToPage("/Accounts/Login");
                }
                if (HttpContext.Session == null)
                {
                    return BadRequest();
                }
                var formSpotId = Request.Form["formSpotId"];
                if (String.IsNullOrEmpty(formSpotId) || formSpotId == "-1")
                {
                    return BadRequest();
                }
                SpotId = Int32.Parse(formSpotId);
                var currentUser = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
                if (currentUser == null)
                {
                    return NotFound();
                }
                if (currentUser.RoleId == 1)
                {
					errorMessage = "Admin or Staff cannot add Booking!";
					return OnGet(SpotId, 0);
				}
				if (NewBooking == null || NewBooking.Started == default || NewBooking.Started.Date <= DateTime.Today || NewBooking.Started.TimeOfDay < TimeSpan.FromHours(9) || NewBooking.Started.TimeOfDay > TimeSpan.FromHours(18))
                {
                    return BadRequest();
                }
                var available = _availableService.GetAvailable(NewBooking.AvailableId);
                if (available == null)
                {
                    return NotFound();
                }
                var service = _serviceService.GetService(available.ServiceId);
                if (service == null)
                {
                    return NotFound();
                }
                NewBooking.Ended = NewBooking.Started.AddMinutes(service.Duration);
                if (NewBooking.Ended.TimeOfDay < TimeSpan.FromHours(9) || NewBooking.Ended.TimeOfDay > TimeSpan.FromHours(18) || _bookingService.IsActiveBookingConflictBySpot(NewBooking.Started, NewBooking.Ended, SpotId))
                {
                    errorMessage = "This time of day is already booked!";
					return OnGet(SpotId, 0);
				}

				List<PetSpaBussinessObject.Booking>? bookingCart = new();
                var json = HttpContext.Session.GetString("BookingCart");

                    // Deserialize JSON to object
                    if (!string.IsNullOrEmpty(json))
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.Preserve,
                            WriteIndented = true 
                        };
                        bookingCart = JsonSerializer.Deserialize<List<PetSpaBussinessObject.Booking>>(json, options);
                }
                if (bookingCart != null)
                {
                    if (bookingCart.Count > 0 && bookingCart.Where(b =>  b.Started < NewBooking.Ended && b.Ended > NewBooking.Started).Any())
                    {
						errorMessage = "A scheduling conflict has occured.";
						return OnGet(SpotId, 0);
					}
                    NewBooking.AccountId = currentUser.Id;
                    NewBooking.Status = true;
                    NewBooking.Available = available;
                    bookingCart.Add(NewBooking);
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true 
                    };
                    HttpContext.Session.Set("BookingCart", JsonSerializer.SerializeToUtf8Bytes(bookingCart, options));
                    BookingCount = bookingCart.Count;
                    HttpContext.Session.SetInt32("BookingCount", BookingCount);
                    successMessage = "Book successfully added";
					return OnGet(SpotId, 0);
                }
                errorMessage = "You already add this to cart!";
				return OnGet(SpotId, 0);
			}
			catch
            {
                return BadRequest();
            }

        }
    }

}
