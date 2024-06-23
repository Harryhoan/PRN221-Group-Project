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
        public Account Account { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || Account == null)
            //{
            //    return Page();
            //}
            Account tempAccount = new Account();
            tempAccount.Name = Account.Name;
            tempAccount.Email = Account.Email;  
            tempAccount.VoucherId = Account.VoucherId;
            tempAccount.Phone= Account.Phone;
            tempAccount.Pass = Account.Pass;
            tempAccount.CountVoucher = Account.CountVoucher;
            tempAccount.Status = Account.Status;
            tempAccount.RoleId = Account.RoleId;
            _account.AddAccount(tempAccount);

            return RedirectToPage("./Index");
        }
    }
}
