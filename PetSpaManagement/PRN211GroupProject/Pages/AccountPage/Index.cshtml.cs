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
    public class IndexModel : PageModel
    {
        private readonly IAccountService _account;

        public IndexModel(IAccountService account)
        {
            _account = account;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_account.GetAllAccount != null)
            {
                Account = _account.GetAllAccount();
            }
        }
    }
}
