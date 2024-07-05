using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
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
        public IActionResult onGet()
        {
            ViewData["RoleId"] = new SelectList(_role.GetAllRole(), "Id", "Name");
            return Page();
        }
        public IList<Account> Account { get;set; } = default!;
        [BindProperty]
        public Account NewAccount { get; set; } = default!;

        public IList<Role> Roles { get; set; } = new List<Role>();
        public async Task OnGetAsync()
        {
            if (_account != null)
            {
                Account = _account.GetAllAccount();
            }
            if (_role != null)
                Roles = _role.GetAllRole();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (NewAccount != null)
            {
                if (NewAccount.RoleId == default)
                {
                    NewAccount.RoleId = 1;
                }
                _account.AddAccount(NewAccount);
            }

            return RedirectToPage("./Index");
        }
    }
}
