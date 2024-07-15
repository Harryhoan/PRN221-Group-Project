using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.BillDetailedService;
using PetSpaService.BillService;

namespace PRN211GroupProject.Pages.Staff.BillPage
{
    public class DetailsModel : PageModel
    {
        private readonly IBillService _billService;
        private readonly IBillDetailedService _billDetailedService;

        public DetailsModel(IBillService billService, IBillDetailedService billDetailedService)
        {
            _billService = billService;
            _billDetailedService = billDetailedService;
        }

      public Bill Bill { get; set; } = default!;
        public BillDetailed BillDetailed { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Staff")
            {
                return Unauthorized();
            }
            if (id == null || _billService.GetBillList() == null)
            {
                return NotFound();
            }

            var bill = _billService.GetBill((int)id);
            var billDetail = _billDetailedService.GetBillDetailed((int)id);
            if (bill == null || billDetail == null)
            {
                return NotFound();
            }
            else 
            {
                Bill = bill;
                BillDetailed = billDetail;
            }
            return Page();
        }
    }
}
