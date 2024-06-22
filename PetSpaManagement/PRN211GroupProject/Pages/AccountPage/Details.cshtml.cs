using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;

namespace PRN211GroupProject.Pages.AccountPage
{
    public class DetailsModel : PageModel
    {
        private readonly IAccountService _account;

        public DetailsModel(IAccountService account)
        {
           _account = account;
        }

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
            else 
            {
                Account = account;
            }
            return Page();
        }
    }
}
