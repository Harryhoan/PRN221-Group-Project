using PetSpaBussinessObject;
using PetSpaDAO;
using PetSpaDaos;
using PetSpaService.AccountService;
using System.Security.Claims;

namespace PRN211GroupProject.Utilities
{
    public class AccountUtilities
    {
        private static AccountUtilities instance = null;
        public static AccountUtilities Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountUtilities();
                }
                return instance;
            }
        }

        public Account? GetAccount(HttpContext httpContext, IAccountService accountService)
        {
            try
            {
                var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
                var emailClaim = claimsIdentity?.FindFirst(ClaimTypes.Email);
                if (emailClaim == null)
                {
                    return null;
                }
                return accountService.GetAccountByEmail(emailClaim.Value);
            }
            catch
            {
                return null;
            }
        }
    }
}
