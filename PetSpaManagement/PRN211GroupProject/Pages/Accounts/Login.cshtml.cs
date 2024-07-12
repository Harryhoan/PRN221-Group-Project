using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using System.Security.Claims;
using PetSpaRepo;
using PetSpaService;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using PetSpaService.AccountService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Update;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using PRN211GroupProject.ViewModel;
namespace PRN211GroupProject.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        [TempData]
        public string errorMessage { get; set; }

        [TempData]
        public string successMessage { get; set; }

        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string pass { get; set; }

        private IAccountService accountService;

        public LoginModel(IAccountService accountSer)
        {
            accountService = accountSer;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()

        {
            Account account = accountService.GetAccountByEmail(email);
            string HashedPass = PasswordHasher.HashPassword(pass);
            var valid = PasswordHasher.VerifyPassword(account.Pass, HashedPass);
            if (account != null && valid == true)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, account.Name),
                    new Claim(ClaimTypes.Role, account.Role.Name)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                Response.Redirect("/Index");
                return Page();

            }
            else
            {
                errorMessage = "Your email or password is incorrect.";
                return Page();
            }
        }


    }
}

