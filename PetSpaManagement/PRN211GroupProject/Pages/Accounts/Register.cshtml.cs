using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PRN211GroupProject.ViewModel;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Mail;
using PRN211GroupProject.Utilities;
using MailKit;
using PetSpaService.MailService;

namespace PRN211GroupProject.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        [TempData]
        public string errorMessage { get; set; }

        [TempData]
        public string successMessage { get; set; }
        public Account? Account { get; set; }
        private IAccountService accountService;
        private readonly ISendMailService _mailService;

        public RegisterModel(IAccountService accountSer, ISendMailService mail)
        {
            accountService = accountSer;
            _mailService = mail;
        }

        public IActionResult Onget()
        {
            Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
            if (Account != null)
                if (Account != null)
                {
                    errorMessage = "Log Out to continue";
                    return RedirectToPage("/Index");
                }
            return Page();
        }
        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                if (accountService.GetAccountByEmail(RegisterViewModel.Email) != null)
                {
                    ModelState.AddModelError("RegisterViewModel.Email", "Email already exists.");
                    return Page();
                }

                Account account = new Account
                {
                    Email = RegisterViewModel.Email.Trim(),
                    Name = FormatUtilities.TrimSpacesPreserveSingle(RegisterViewModel.Name),
                    Pass = RegisterViewModel.Pass,
                    Phone = RegisterViewModel.Phone,
                    RoleId = 2,
                    Status = true,
                    Created = DateTime.Now,
                    CountVoucher = 0,
                    VoucherId = null
                };
                accountService.AddAccount(account);
                successMessage = "Registered Successfully.Login to continue.";
                MailData mailData = new();
                mailData.ReceiverEmail = RegisterViewModel.Email;
                mailData.ReceiverName = RegisterViewModel.Name;
                mailData.Title = "Registered Successfully";
                mailData.Body = $"</br>Dear {mailData.ReceiverName},</br>Thank you for registering with our system. We hope you enjoy using it!</br>Best regards.";
                _mailService.SendMail(mailData);
                return RedirectToPage("Login");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
