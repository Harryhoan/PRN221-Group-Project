using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.AvailableService;
using PetSpaService.BookingService;
using PetSpaService.SpotService.SpotService;
using System.Security.Claims;

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
        [BindProperty]
        public int SpotId { get; set; } = -1;
        public IActionResult OnGet(int? spotId, int? offset) 
        {
            try
            {
                if (User.Identity != null && !String.IsNullOrEmpty(User.Identity.Name) && User.Identity.IsAuthenticated)
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

                    ViewData["spotList"] = spotList.Select(spot => new SelectListItem
                    {
                        Value = spot.Id.ToString(),
                        Text = spot.Name
                    }).ToList();

                    ViewData["availableList"] = _availableService.GetAvailableListBySpot((int)spotId).Select(available => new SelectListItem
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
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }

        public IActionResult OnPost()
        {
            try 
            {
                if (User.Identity == null || String.IsNullOrEmpty(User.Identity.Name) || !User.Identity.IsAuthenticated) 
                {
                    return Unauthorized();
                }

                var formSpotId = Request.Form["formSpotId"];
                if (String.IsNullOrEmpty(formSpotId) || formSpotId == "-1")
                {
                    return BadRequest();
                }
                SpotId = Int32.Parse(formSpotId);
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                var emailClaim = claimsIdentity?.FindFirst(ClaimTypes.Email);
                if (emailClaim == null)
                {
                    return Unauthorized();
                }
                var currentUser = _accountService.GetAccountByEmail(emailClaim.Value);
                if (currentUser == null)
                {
                    return NotFound();
                }
                if (currentUser.RoleId == 1)
                {
                    return Unauthorized();
                }
                if (NewBooking == null || NewBooking.Started == default || NewBooking.Started.Date <= DateTime.Today || NewBooking.Started.TimeOfDay < TimeSpan.FromHours(9) || NewBooking.Started.TimeOfDay > TimeSpan.FromHours(18))
                {
                    return BadRequest();
                }                         
                var available = _availableService.GetAvailable(NewBooking.AvailableId);
                if (available == null) {
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
                    return BadRequest();
                }
                NewBooking.AccountId = currentUser.Id;
                NewBooking.Status = true;
                NewBooking.Created = DateTime.Now;
                _bookingService.AddBooking(NewBooking);
                return RedirectToPage(Request.Path);
                
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}
