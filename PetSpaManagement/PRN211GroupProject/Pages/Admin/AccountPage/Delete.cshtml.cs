using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;

namespace PRN211GroupProject.Pages.AccountPage
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountService _account;

        public DeleteModel(IAccountService account)
        {
            _account = account;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
            {
                return Unauthorized();
            }
            if (id == null || _account.GetAllAccount() == null)
            {
                return NotFound();
            }

            var account = _account.GetAccount((int)id);
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                if (id == null || _account.GetAllAccount() == null)
                {
                    return Page();
                }
                _account.DeleteAccount(id);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return RedirectToPage("./Index");
        }
    }
}
