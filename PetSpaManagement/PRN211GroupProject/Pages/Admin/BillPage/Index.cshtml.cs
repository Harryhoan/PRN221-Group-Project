using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;
using PetSpaService.AvailableService;
using PetSpaService.BillService;

namespace PRN211GroupProject.Pages.Admin.BillPage
{
    public class IndexModel : PageModel
    {
        private readonly IBillService _billService;
        private readonly IAccountService _accountService;
        private readonly IAvailableService _availableService;

        public IndexModel(IBillService billService, IAccountService accountService, IAvailableService availableService)
        {
            _billService = billService;
            _accountService = accountService;
            _availableService = availableService;
        }

        public IList<Bill> Bill { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
            {
                return Unauthorized();
            }
            try
            {
                if(_billService == null)
                {
                    return BadRequest();
                }
                Bill = _billService.GetBillList();
            }
            catch
            {
                return BadRequest();
            }
            //ViewData["Acc"] = new SelectList(_accountService.GetAllAccount(), "Id", "Name");
            return Page();
        }
    }
}
