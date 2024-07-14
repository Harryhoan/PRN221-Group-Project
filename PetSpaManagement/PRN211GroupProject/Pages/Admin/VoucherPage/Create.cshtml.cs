using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.VoucherService.VoucherService;

namespace PRN211GroupProject.Pages.Admin.VoucherPage
{
    public class CreateModel : PageModel
    {
        private readonly IVoucherService _voucherService;

        public CreateModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Voucher Voucher { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _voucherService.GetVoucherList() == null || Voucher == null)
            {
                return Page();
            }
            _voucherService.AddVoucher(Voucher);
            return RedirectToPage();
        }
    }
}
