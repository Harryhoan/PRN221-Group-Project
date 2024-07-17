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
        public Account? Account { get; set; }
        [TempData]
        public string errorMessage { get; set; }
        [TempData]
        public string successMessage { get; set; }

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

                if (ProfileViewModel == null)
                {
                    return BadRequest();
                }
                if (Account.Email != ProfileViewModel.Email)
                {
                    if (accountService.GetAccountByEmail(ProfileViewModel.Email) != null)
                    {
                        errorMessage = "Email already exists.";
                        return Page();
                    }
                }
                Account.Email = ProfileViewModel.Email.Trim();
                Account.Name = FormatUtilities.TrimSpacesPreserveSingle(ProfileViewModel.Name);
                Account.Phone = ProfileViewModel.Phone;
                accountService.UpdateAccount(Account);
                successMessage = "The profile is successfully updated.";
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
                if (!accountService.VerifyPassword(ChangePasswordViewModel.OldPass, Account?.Pass))
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
                successMessage = "Password successfully change";
                return Page();
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
