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
using PetSpaService.VoucherService.VoucherService;

namespace PRN211GroupProject.Pages.Admin.VoucherPage
{
    public class DeleteModel : PageModel
    {
        private readonly IVoucherService _voucherService;

        public DeleteModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [BindProperty]
        public Voucher Voucher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
            {
                return Unauthorized();
            }
            if (id == null || _voucherService.GetVoucherList() == null)
            {
                return NotFound();
            }
            var voucher = _voucherService.GetVoucher((int)id);

            if (voucher == null)
            {
                return NotFound();
            }
            else
            {
                Voucher = voucher;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _voucherService.GetVoucherList() == null)
            {
                return NotFound();
            }

            _voucherService.DeleteVoucher((int)id);

            return RedirectToPage();
        }
    }
}
