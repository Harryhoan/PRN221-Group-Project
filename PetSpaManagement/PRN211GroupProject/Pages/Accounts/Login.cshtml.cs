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
using Microsoft.AspNetCore.Identity;
using PRN211GroupProject.ViewModel;
using PetSpaService.VoucherService.VoucherService;
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
        private readonly IVoucherService voucherService;

        public LoginModel(IAccountService account, IVoucherService voucher)
        {
            accountService = account;
            voucherService = voucher;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()

        {
            Account account = accountService.Login(email,pass);
            if (account != null)
            {
                if (account.VoucherId != null) 
                {
                    Voucher voucher = voucherService.GetVoucher((int)account.VoucherId);
                    if (voucher.Expired >= DateTime.Now)
                    {
                        account.VoucherId = null;
                        accountService.UpdateAccount(account);
                    }
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, account.Name),
                    new Claim(ClaimTypes.Role, account.Role.Name)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                Response.Redirect("/");
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

