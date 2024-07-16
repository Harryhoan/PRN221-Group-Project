using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PetSpaService.RolesService;
using PRN211GroupProject.Utilities;

namespace PRN211GroupProject.Pages.AccountPage
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IRoleService roleService;

        public IndexModel(IAccountService account, IRoleService role)
        {
            accountService = account;
            roleService = role;
        }

        public IList<Account> Accounts { get; set; } = default!;

        [BindProperty]
        public Account NewAccount { get; set; } = default!;
        public Account? Account { get; set; }
        [TempData]
        public string errorMessage { get; set; }

        public IList<Role> Roles { get; set; } = new List<Role>();

        public async Task<IActionResult> OnGetAsync()
        {
            Account = AccountUtilities.Instance.GetAccount(HttpContext, accountService);
            if (Account != null)
            {
                var roleClaim = User.FindFirst(ClaimTypes.Role);

                if (roleClaim?.Value.ToString() != "Admin")
                {
                    errorMessage = "You do not have permission to access the Admin page.";
                    return RedirectToPage("/Index");
                }
                if (accountService != null)
                {
                    Accounts = accountService.GetAllAccount();
                }

                if (roleService != null)
                {
                    Roles = roleService.GetAllRole();
                }

                ViewData["RoleId"] = new SelectList(roleService.GetAllRole(), "Id", "Name");

                return Page();
            }
            else
            {
                    errorMessage = "You must login first";
                    return RedirectToPage("/Accounts/Login");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (NewAccount != null)
            {
                accountService.AddAccount(NewAccount);
            }
            return RedirectToPage();
        }
    }
}
