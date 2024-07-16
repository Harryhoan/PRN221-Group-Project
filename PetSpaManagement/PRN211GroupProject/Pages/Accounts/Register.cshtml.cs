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
        private IAccountService accountService;
        public Account? Account { get; set; }


        public RegisterModel(IAccountService accountSer)
        {
            accountService = accountSer;
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
                Email = RegisterViewModel.Email,
                Name = RegisterViewModel.Name,
                Pass = RegisterViewModel.Pass,
                Phone = RegisterViewModel.Phone,
                RoleId = 2,
                Status = true,
                CountVoucher = 0,
                VoucherId = null
            };
            accountService.AddAccount(account);
            successMessage = "Registered Successfully.Login to continue.";
            return RedirectToPage("Login");
        }
    }
}
