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
        public IActionResult OnGet()
        {
            Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
            if (Account == null)
            {
                return Unauthorized();
            }
            return Page();
        }
        public IActionResult OnPostSaveProfile()
        {
            try
            {
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
                accountService.UpdateAccount(Account);
                return RedirectToPage("/Index");
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
                if (Account?.Pass != ChangePasswordViewModel.OldPass)
                {
                    ModelState.AddModelError("ChangePasswordViewModel.OldPass", "Your password is incorrect.");
                    return Page();
                }
                Account.Pass = ChangePasswordViewModel.NewPass;
                accountService.UpdateAccount(Account);
                return RedirectToPage("/Index");
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
