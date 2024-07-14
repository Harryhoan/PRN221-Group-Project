using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.AvailableService;
using PetSpaService.BillDetailedService;
using PetSpaService.BillService;
using PetSpaService.BookingService;
using PetSpaService.SpotService.SpotService;
using PetSpaService.VoucherService.VoucherService;

namespace PRN211GroupProject.Pages.Accounts.History
{
    public class DetailsModel : PageModel
    {
        private readonly IBillDetailedService billDetailedService;
        private readonly IBillService billService;
        private readonly IAccountService accountService;
        private readonly IBookingService bookingService;
        private readonly IAvailableService availableService;
        private readonly IServiceService serviceService;
        private readonly ISpotService spotService;
        private readonly IVoucherService voucherService;


        public DetailsModel(IBillDetailedService billDetailed, IAccountService account, IBillService bill, IBookingService booking, IAvailableService available, IServiceService service, ISpotService spot, IVoucherService voucher)
        {
            billDetailedService = billDetailed;
            billService = bill;
            accountService = account;
            bookingService = booking;
            availableService = available;
            serviceService = service;
            spotService = spot;
            voucherService = voucher;
        }
        public IList<BillDetailed> BillDetails { get; set; } = new List<BillDetailed>();

        public IActionResult OnGet(int id)
        {
            if (billDetailedService == null)
            {
                return NotFound("BillDetailedService not found");
            }

            var billDetails = billDetailedService.GetBillDetailsByBillId(id);
            if (billDetails == null)
            {
                return NotFound("Bill details not found");
            }

            foreach (var item in billDetails)
            {
                if (item != null)
                {
                    item.Booking = bookingService.GetBooking(item.BookingId);
                    if (item.Booking != null)
                    {
                        item.Booking.Available = availableService.GetAvailable(item.Booking.AvailableId);
                        if (item.Booking.Available != null)
                        {
                            item.Booking.Available.Service = serviceService.GetService(item.Booking.Available.ServiceId);
                            item.Booking.Available.Spot = spotService.GetSpot(item.Booking.Available.SpotId);
                        }
                    }
                    BillDetails.Add(item);
                }
            }
            return Page();
        }
    }
}