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
        private readonly IAccountService accountService;
        private readonly IRoleService roleService;

        public IndexModel(IAccountService account, IRoleService role)
        {
            accountService = account;
            roleService = role;
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
            if (accountService != null)
            {
                Account = accountService.GetAllAccount();
            }

            if (roleService != null)
            {
                Roles = roleService.GetAllRole();
            }

            ViewData["RoleId"] = new SelectList(roleService.GetAllRole(), "Id", "Name");

            return Page();
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
