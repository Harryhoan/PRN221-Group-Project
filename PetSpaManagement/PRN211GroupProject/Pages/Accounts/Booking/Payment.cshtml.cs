using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit.Cryptography;
using PetSpaBussinessObject;
using PetSpaRepo.AvailableRepository;
using PetSpaRepo.BookingRepository;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.AvailableService;
using PetSpaService.BillDetailedService;
using PetSpaService.BillService;
using PetSpaService.BookingService;
using PetSpaService.MailService;
using PetSpaService.SpotService.SpotService;
using PetSpaService.VoucherService.VoucherService;
using PRN211GroupProject.Utilities;
using PRN211GroupProject.ViewModel;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
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
        private readonly ISendMailService _mailService;


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
        [TempData]
        public string errorMessage { get; set; }
        [TempData]
        public string successMessage { get; set; }


        [BindProperty]
        public List<BillDetailed>? BillDetaileds { get; set; } = new List<BillDetailed>();
        public List<PetSpaBussinessObject.Booking>? Bookings { get; set; }
        [BindProperty]
        public Account? Account { get; set; }
        [BindProperty]
        public double Discount { get; set; } = 0;
        [BindProperty]
        public double Sum { get; set; } = 0;
        [BindProperty]
        public int SelectedVoucherId { get; set; } = 0;
        public IActionResult OnGet()
        {
            try
            {
                Sum = 0;
                Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
                if (Account != null)
                {
                    if (Account.VoucherId != null)
                    {
                        Voucher voucher = voucherService.GetVoucher((int)Account.VoucherId);
                        if (voucher.Expired <= DateTime.Now)
                        {
                            Account.VoucherId = null;
                            Account.Voucher = null;
                            accountService.UpdateAccount(Account);
                        }
                        else
                        {
                            Account.Voucher = voucherService.GetVoucher((int)Account.VoucherId);
                        }
                    }
                    else
                    {
                        Account.Voucher = null;
                    }
                }
                else
                {
                    errorMessage = "You must login first";
                    return RedirectToPage("/Accounts/Login");

                }

            }

            catch
            {
                return BadRequest();
            }


            try
            {
                var bookingCartBytes = HttpContext.Session.Get("BookingCart");
                if (bookingCartBytes != null)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true
                    };
                    Bookings = JsonSerializer.Deserialize<List<PetSpaBussinessObject.Booking>>(bookingCartBytes, options).OrderByDescending(b => b.Started).ToList();
                }
            }
            catch (JsonException ex)
            {
                errorMessage = ($"Error deserializing JSON: {ex.Message}");
                return Page();
            }
            if (Bookings != null && BillDetaileds != null)
            {
                foreach (var item in Bookings)
                {
                    BillDetailed bd = new();
                    bd.Booking = item;
                    var available = availableService.GetAvailable(item.AvailableId);
                    bd.Cost = available.Service.Price;
                    Sum += bd.Cost;
                    bd.Booking.Available = available;
                    BillDetaileds.Add(bd);
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
                errorMessage = "An error occur ,Cant apply voucher";
                return Page();
            }
        }

        public IActionResult OnPostBill()
        {

            try
            {
                Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
                if (Account != null)
                {

                    var billDetailedJson = Request.Form["billDetailedJson"];
                    if (!string.IsNullOrEmpty(billDetailedJson))
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.Preserve,
                            WriteIndented = true
                        };
                        BillDetaileds = JsonSerializer.Deserialize<List<BillDetailed>>(billDetailedJson, options);
                    }
                    if (BillDetaileds != null && BillDetaileds.Count > 0)
                    {

                        Bill bill = new Bill();
                        bill.Total = Sum - Discount;
                        bill.AccId = Account.Id;
                        bill.Started = BillDetaileds[0].Booking.Started;
                        bill.VoucherId = SelectedVoucherId == 0 ? null : SelectedVoucherId;
                        bill.Created = DateTime.Now;
                        billService.AddBill(bill);
                        if (bill != null && bill.Id > 0)
                        {
                            foreach (var item in BillDetaileds)
                            {
                                item.Booking.Created = DateTime.Now;
                                bookingService.AddBooking(item.Booking);
                                item.BookingId = item.Booking.Id;
                                item.BillId = bill.Id;
                                billDetailedService.AddBillDetailed(item);
                            }
                        }

                        if (SelectedVoucherId != 0)
                        {
                            Account.VoucherId = null;
                            accountService.UpdateAccount(Account);
                        }
                        successMessage = "Payment Success,Thanks for use our service";
                        MailData mailData = new MailData();
                        mailData.ReceiverEmail = Account.Email;
                        mailData.ReceiverName = Account.Name;
                        mailData.Title = "Payment Success, Thanks for using our service";

                        StringBuilder bodyBuilder = new StringBuilder();
                        bodyBuilder.AppendLine($"Dear {mailData.ReceiverName},</br>");
                        bodyBuilder.AppendLine("Here are your recent Bookings:</br>");

                        foreach (var item in BillDetaileds)
                        {
                            if (item != null && item.Booking != null && item.Booking.Available != null &&
                                item.Booking.Available.Service != null && item.Booking.Available.Spot != null)
                            {
                                bodyBuilder.AppendLine($"<b>Service:</b> {item.Booking.Available.Service.Name}</br>");
                                bodyBuilder.AppendLine($"<b>Spot:</b> {item.Booking.Available.Spot.Name}</br>");
                                bodyBuilder.AppendLine($"<b>Started:</b> {item.Booking.Started}</br>");
                                bodyBuilder.AppendLine($"<b>Duration:</b> ${item.Booking.Available.Service.Duration} Minutes</br>");
                                bodyBuilder.AppendLine($"<b>Cost:</b> {item.Cost}</br>");
                                bodyBuilder.AppendLine("</br>");
                            }
                        }

                        bodyBuilder.AppendLine($"<b>Discount:</b> {Discount}</br>");
                        bodyBuilder.AppendLine($"<b>Total:</b> {Sum - Discount}</br>");
                        bodyBuilder.AppendLine("</br>");
                        bodyBuilder.AppendLine("Please ensure timely payment of these bills. If you have any questions, feel free to contact us.</br>");
                        bodyBuilder.AppendLine("Best regards.");

                        mailData.Body = bodyBuilder.ToString();
                        _mailService.SendMail(mailData);
                        HttpContext.Session.Clear();
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    errorMessage = "You must login first";
                    return RedirectToPage("/Accounts/Login");
                }
            }
            catch(Exception ex) 
            {
                successMessage= null;
                return BadRequest();
            }
        }


    }
}
