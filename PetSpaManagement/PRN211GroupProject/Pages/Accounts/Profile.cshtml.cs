using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PRN211GroupProject.Utilities;
using PRN211GroupProject.ViewModel;

namespace PRN211GroupProject.Pages.Accounts
{
    public class ProfileModel : PageModel
    {
        private readonly IAccountService accountService;
        public ProfileModel(IAccountService account)
        {
            accountService = account;
        }
        [BindProperty]
        public ProfileViewModel ProfileViewModel { get; set; }
        [BindProperty]
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        [BindProperty]
        public Account? Account { get; set; }
        [TempData]
        public string errorMessage { get; set; }

        public IActionResult OnGet()
        {
            Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
            if (Account == null)
            {
                errorMessage = "You must login first";
                return RedirectToPage("/Accounts/Login");
            }
            return Page();
        }
        public IActionResult OnPostSaveProfile()
        {
            try
            {
                Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
                if (Account == null)
                {
                    errorMessage = "You must login first";
                    return RedirectToPage("/Accounts/Login");
                }

                if (Account?.Email == ProfileViewModel.Email)
                {
                    if (accountService.GetAccountByEmail(ProfileViewModel.Email) != null)
                    {
                        ModelState.AddModelError("ProfileViewModel.Email", "Email already exists.");
                        return Page();
                    }
                }
                Account.Email = ProfileViewModel.Email;
                Account.Name = ProfileViewModel.Name;
                Account.Phone = ProfileViewModel.Phone;
                Account.Status = true;
                accountService.UpdateAccount(Account);
                errorMessage = "Profile successfully change";
                return Page();
            }
            catch
            {
                return BadRequest();
            }
        }
        public IActionResult OnPostChangePassword()
        {
            try
            {
                Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
                if (Account == null)
                {
                    errorMessage = "You must login first";
                    return RedirectToPage("/Accounts/Login");
                }
                if (!accountService.VerifyPassword(ChangePasswordViewModel.OldPass,Account?.Pass))
                {
                    errorMessage = "Your password is incorrect";
                    return Page();
                }
                else if (accountService.VerifyPassword(ChangePasswordViewModel.NewPass, Account?.Pass))
                {
                    errorMessage = "New password cannot be the same as the old password.";
                    return Page();
                }
                Account.Pass = ChangePasswordViewModel.NewPass;
                accountService.UpdateAccount(Account);
                Account.Status = true;
                errorMessage = "Password successfully change";
                return Page();
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
