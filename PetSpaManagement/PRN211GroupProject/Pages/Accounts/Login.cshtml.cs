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
            Account account = accountService.Login(email, pass);

            if (account != null)    
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, account.Name),
                    new Claim(ClaimTypes.MobilePhone, account.Phone),
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

