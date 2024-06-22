using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AccountService;

namespace PRN211GroupProject.Pages
{
    public class RegisterModel : PageModel
    {
        public string errorMessage;
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Pass { get; set; }

        [BindProperty]
        public string confirmPassword { get; set; }
        [BindProperty]
        public string Phone { get; set; }

        private IAccountService accountService;

        public RegisterModel(IAccountService accountSer)
        {
            accountService = accountSer;
        }
        public void OnPost()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Pass) || string.IsNullOrEmpty(confirmPassword))
            {
                errorMessage = "Please fill in all required fields.";
                return;
            }
            if (Pass != confirmPassword)
            {
                errorMessage = "Password and confirm password do not match.";
                return;
            }
            if (!ModelState.IsValid)
            {
                // If ModelState is not valid due to other validation errors, handle them here
                return;
            }
            Account account = new Account();
            account.Email = Email;
            account.Name = Name;
            account.Pass = Pass;
            account.Phone = Phone;
            account.RoleId = 1;
            account.Status = true;
            account.CountVoucher = 0;
            account.VoucherId = null;
            accountService.AddAccount(account);
            errorMessage = "Registered Successfull,Login to continue";
            Response.Redirect("Login");
        }
    }
}
