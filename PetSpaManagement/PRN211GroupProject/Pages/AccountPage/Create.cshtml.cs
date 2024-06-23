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

namespace PRN211GroupProject.Pages.AccountPage
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _account;

        public CreateModel(IAccountService account)
        {
            _account = account;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        
        {
            if (!ModelState.IsValid || Account == null || Account == null)
            {
                return Page();
            }

            _account.AddAccount(Account);

            return RedirectToPage("./Index");
        }
    }
}
