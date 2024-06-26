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

namespace PRN211GroupProject.Pages.AccountPage
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _account;
        private readonly IRoleService _role;

        public CreateModel(IAccountService account, IRoleService role)
        {
            _account = account;
            _role = role;
        }

        public IActionResult OnGet()
        {
            ViewData["RoleId"] = new SelectList(_role.GetAllRole(), "Id", "Name");
            ViewData["VoucherId"] = new SelectList(_account.GetAllAccount(), "VoucherId", "Voucher");
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!modelstate.isvalid || account == null)
            //{
            //    return page();
            //}
            Account.Status = true;
            _account.AddAccount(Account);

            return RedirectToPage("./Index");
        }
    }
}
