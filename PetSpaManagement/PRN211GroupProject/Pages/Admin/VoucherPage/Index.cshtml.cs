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
using PetSpaService.VoucherService.VoucherService;

namespace PRN211GroupProject.Pages.Admin.VoucherPage
{
    public class IndexModel : PageModel
    {
        private readonly IVoucherService _voucherService;

        public IndexModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public IList<Voucher> Voucher { get;set; } = default!;
        [BindProperty]
        public Voucher NewVoucher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
            {
                return Unauthorized();
            }
            if (_voucherService != null)
            {
                Voucher = _voucherService.GetVoucherList();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (NewVoucher != null)
            {
                _voucherService.AddVoucher(NewVoucher);
            }
            return RedirectToPage("/Index");
        }
    }
}
