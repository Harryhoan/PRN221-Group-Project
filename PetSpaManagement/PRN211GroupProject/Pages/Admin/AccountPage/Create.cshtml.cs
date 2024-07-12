using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;
using PetSpaService.RolesService;

namespace PRN211GroupProject.Pages.Admin.AccountPage
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IRoleService roleService;

        public CreateModel(IAccountService account, IRoleService role)
        {
            accountService = account;
            roleService = role;
        }

        public IActionResult OnGet()
        {
            ViewData["RoleId"] = new SelectList(roleService.GetAllRole(), "Id", "Name");
            ViewData["VoucherId"] = new SelectList(accountService.GetAllAccount(), "VoucherId", "Voucher");
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || accountService.GetAllAccount() == null || Account == null)
            {
                return Page();
            }
            accountService.AddAccount(Account);

            return RedirectToPage("./Index");
        }
    }
}
