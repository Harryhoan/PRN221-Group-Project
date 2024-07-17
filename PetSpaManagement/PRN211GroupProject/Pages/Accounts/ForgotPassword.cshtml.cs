using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Common;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PetSpaService.MailService;
using PRN211GroupProject.Utilities;
using PRN211GroupProject.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PRN211GroupProject.Pages.Accounts
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ISendMailService _mailService;

        public ForgotPasswordModel(IAccountService accountService, ISendMailService mailService)
        {
            _accountService = accountService;
            _mailService = mailService;
        }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Token { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }


        public IActionResult OnGet()
        {
            var account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
            if (account != null)
            {
                ErrorMessage = "You are already logged in.";
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostVerifyEmail()
        {
            var Account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
            if (Account != null)
            {
                ErrorMessage = "You are already logged in.";
                return RedirectToPage("/Index");
            }
            if (string.IsNullOrEmpty(Email))
            {
                ErrorMessage = "Email is required.";
                return Page();
            }

            var account = _accountService.GetAccountByEmail(Email);
            if (account == null)
            {
                ErrorMessage = "Email not found in our records.";
                return Page();
            }
            string token = GenerateRandomString();
            MailData mailData = new MailData
            {
                ReceiverEmail = account.Email,
                ReceiverName = account.Name,
                Title = "Password Reset Token",
                Body = $"Dear {account.Name},\n\nThis is your token to reset your password: {token}.\n\nBest regards."
            };

            if (!_mailService.SendMail(mailData))
            {
                ErrorMessage = "Failed to send reset token. Please try again later.";
                return Page();
            }
            TempData["ResetToken"] = token;
            TempData["ResetEmail"] = account.Email;
            SuccessMessage = "Reset token sent to your email. Please check your inbox.";
            return RedirectToPage("/Accounts/ForgotPassword"); 
        }

        public IActionResult OnPostVerifyToken()
        {
            var account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
            if (account != null)
            {
                ErrorMessage = "You are already logged in.";
                return RedirectToPage("/Index");
            }
            string storedToken = TempData["ResetToken"] as string;
            string storedEmail = TempData["ResetEmail"] as string;
            if (string.IsNullOrEmpty(Token) || storedToken != Token)
            {
                ErrorMessage = "Invalid token. Please check and try again.";
                return Page();
            }
            else
            {
                var accounts = _accountService.GetAccountByEmail(storedEmail);
                if (accounts != null)
                {
                    accounts.Pass = _accountService.HashPassword("123");
                    _accountService.UpdateAccount(accounts);
                }
                MailData mailData = new MailData
                {
                    ReceiverEmail = accounts.Email,
                    ReceiverName = accounts.Name,
                    Title = "Reset Password",
                    Body = $"Dear {accounts.Name},\n\nYour Password hasbeen reset to: 123.\n\nBest regards."
                };

                if (!_mailService.SendMail(mailData))
                {
                    ErrorMessage = "Failed to send reset token. Please try again later.";
                    return Page();
                }
                    SuccessMessage = "Your password has been reset. Please check your inbox."; 

            }
            return RedirectToPage("/Accounts/Login", new { email = Email, token = Token });
        }

        public string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
