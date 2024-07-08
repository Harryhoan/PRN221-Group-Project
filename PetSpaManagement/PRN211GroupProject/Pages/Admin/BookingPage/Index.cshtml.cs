using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.BookingService;

namespace PRN211GroupProject.Pages.Admin.BookingPage
{
    public class IndexModel : PageModel
    {
        private readonly IBookingService _booking;

        public IndexModel(IBookingService booking)
        {
            _booking = booking;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_booking.GetBookingList() != null)
            {
                Booking = _booking.GetBookingList();
            }
        }
    }
}
