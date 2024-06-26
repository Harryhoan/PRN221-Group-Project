using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PRN211GroupProject.ViewModel;
using System.Text.RegularExpressions;
using System.Web;

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

        public RegisterModel(IAccountService accountSer)
        {
            accountService = accountSer;
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (accountService.GetAccountByEmail(RegisterViewModel.Email) != null)
            {
                //RegisterViewModel.Email = "An account with this email already exists.";
                //ModelState.AddModelError("Duplicate Email Error", RegisterViewModel.Email);
                return Page();
            }
            
            Account account = new Account
            {
                Email = RegisterViewModel.Email,
                Name = RegisterViewModel.Name,
                Pass = RegisterViewModel.Pass,
                Phone = RegisterViewModel.Phone,
                RoleId = 1,
                Status = true,
                CountVoucher = 0,
                VoucherId = null
            };

            accountService.AddAccount(account);

            successMessage = "Registered Successfully. Login to continue.";
            return RedirectToPage("Login");
        }
    }
}
