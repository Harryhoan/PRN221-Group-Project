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

namespace PRN211GroupProject.Pages.AccountPage
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _account;
        private readonly IRoleService _role;

        public IndexModel(IAccountService account, IRoleService role)
        {
            _account = account;
            _role = role;
        }

        public IList<Account> Account { get; set; } = default!;

        [BindProperty]
        public Account NewAccount { get; set; } = default!;

        public IList<Role> Roles { get; set; } = new List<Role>();

        public async Task<IActionResult> OnGetAsync()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
            {
                return Unauthorized();
            }
            if (_account != null)
            {
                Account = _account.GetAllAccount();
            }

            if (_role != null)
            {
                Roles = _role.GetAllRole();
            }

            ViewData["RoleId"] = new SelectList(_role.GetAllRole(), "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int accountID)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            // Check if the user has an admin role
            if (!_account.IsAdmin(accountID))
            {
                return Forbid(); // Use Forbid to indicate the user is authenticated but not authorized
            }
            if (NewAccount != null)
            {
                if (NewAccount.RoleId == default)
                {
                    NewAccount.RoleId = 1;
                }

                _account.AddAccount(NewAccount);
            }

            return RedirectToPage("/Index");
        }
    }
}
