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

namespace PRN211GroupProject.Pages.AccountPage
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _account;

        public EditModel(IAccountService account)
        {
            _account = account;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _account.GetAllAccount() == null)
            {
                return NotFound();
            }

            var account = _account.GetAccount(id);
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                 _account.UpdateAccount(Account);
            }
            catch (Exception ex)
            {
                //if (!AccountExists(Account.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return RedirectToPage("./Index");
        }

        //private bool AccountExists(int id)
        //{
        //  return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
