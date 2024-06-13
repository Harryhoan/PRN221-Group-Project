using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaRepo;
using PetSpaService;

namespace PRN211GroupProject.Pages
{
    public class LoginModel : PageModel
    {
        public string errorMessage;
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

        public void OnPost()
        {
            Account account = accountService.GetAccountByEmail(email, pass);

            if (account != null)
            {
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetInt32("RoleID", account.RoleId);
                Response.Redirect("Index");
            }
            else
            {
                errorMessage = "Login failed, your email or password is incorrect";
                Response.Redirect("Login");
            }
        }


    }
}

