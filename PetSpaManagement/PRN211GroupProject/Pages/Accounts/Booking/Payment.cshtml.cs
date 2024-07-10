using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaRepo.AvailableRepository;
using PetSpaRepo.BookingRepository;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.AvailableService;
using PetSpaService.BillDetailedService;
using PetSpaService.BillService;
using PetSpaService.BookingService;
using PetSpaService.SpotService.SpotService;
using PetSpaService.VoucherService.VoucherService;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PRN211GroupProject.Pages.Accounts.Booking
{
    public class PaymentModel : PageModel
    {
        private readonly IBillDetailedService billDetailedService;
        private readonly IBillService billService;
        private readonly IAccountService accountService;
        private readonly IBookingService bookingService;
        private readonly IAvailableService availableService;
        private readonly IServiceService serviceService;
        private readonly ISpotService spotService;
        private readonly IVoucherService voucherService;


        public PaymentModel(IBillDetailedService billDetailed, IAccountService account, IBillService bill, IBookingService booking, IAvailableService available, IServiceService service, ISpotService spot, IVoucherService voucher)
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
        [BindProperty]
        public List<BillDetailed> billDetaileds { get; set; } = new List<BillDetailed>();
        public List<PetSpaBussinessObject.Booking>? Bookings { get; set; }
        [BindProperty]
        public Account Account { get; set; }
        [BindProperty]
        public double Discount { get; set; } = 0;
        [BindProperty]
        public double Sum { get; set; } = 0;
        [BindProperty]
        public int SelectedVoucherId { get; set; } = 0;
        public IActionResult OnGet()
        {
            Sum = 0;
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var emailClaim = claimsIdentity?.FindFirst(ClaimTypes.Email);
            if (emailClaim == null)
            {
                return Unauthorized();
            }
            else
            {
                Account = accountService.GetAccountByEmail(emailClaim.Value);
                if (Account != null)
                {
                    if (Account.VoucherId != null)
                    {
                        Account.Voucher = voucherService.GetVoucher((int)Account.VoucherId);
                    }
                    else
                    {
                        Account.Voucher = null;
                    }
                }
                else
                {
                    return NotFound();
                }



            }
            try
            {
                var bookingCartBytes = HttpContext.Session.Get("BookingCart");
                if (bookingCartBytes != null)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true // for readability during debugging
                    };
                    Bookings = JsonSerializer.Deserialize<List<PetSpaBussinessObject.Booking>>(bookingCartBytes, options).OrderByDescending(b => b.Started).ToList();
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
            if (Bookings != null)
            {
                foreach (var item in Bookings)
                {
                    BillDetailed bd = new();
                    bd.Booking = item;
                    var available = availableService.GetAvailable(item.AvailableId);
                    bd.Cost = available.Service.Price;
                    Sum += bd.Cost;
                    bd.Booking.Available = available;
                    billDetaileds.Add(bd);
                }
            }
            return Page();
        }
        public IActionResult OnPostApplyVoucher()
        {
            try
            {
                if (SelectedVoucherId != 0 && Discount == 0)
                {
                    var selectedVoucher = voucherService.GetVoucher(SelectedVoucherId);

                    if (selectedVoucher != null)
                    {
                        Discount = (Sum * selectedVoucher.Discount) / 100;
                    }
                }
                else if (SelectedVoucherId == 0)
                {
                    Discount = 0;
                }
                return OnGet();
            }
            catch
            {
                return BadRequest();
            }
        }
        public IActionResult OnPostBill()
        {
            try
            {
                var bookingCartBytes = HttpContext.Session.Get("BookingCart");
                if (bookingCartBytes != null)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true // for readability during debugging
                    };
                    Bookings = JsonSerializer.Deserialize<List<PetSpaBussinessObject.Booking>>(bookingCartBytes, options).OrderByDescending(b => b.Started).ToList();
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
            if (Bookings != null)
            {
                foreach (var item in Bookings)
                {
                    BillDetailed bd = new();
                    bd.Booking = item;
                    var available = availableService.GetAvailable(item.AvailableId);
                    bd.Cost = available.Service.Price;
                    Sum += bd.Cost;
                    bd.Booking.Available = available;
                    billDetaileds.Add(bd);
                }
            }

            if (billDetaileds != null && billDetaileds.Count > 0)
            {
                Bill bill = new Bill();
                bill.Total = Sum - Discount;
                bill.AccId = Account.Id;
                bill.Started = billDetaileds[0].Booking.Started;
                bill.VoucherId = SelectedVoucherId;
                billService.AddBill(bill);

                if (bill != null && bill.Id > 0)
                {
                    foreach (var item in billDetaileds)
                    {
                        item.Booking.Created = DateTime.Now;
                        bookingService.AddBooking(item.Booking);
                        item.BookingId = item.Booking.Id;
                        item.BillId = bill.Id;
                        billDetailedService.AddBillDetailed(item);
                    }
                }
                HttpContext.Session.Clear();
                return RedirectToPage("/index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
