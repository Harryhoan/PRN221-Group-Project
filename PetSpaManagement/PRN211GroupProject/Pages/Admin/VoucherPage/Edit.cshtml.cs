﻿using System;
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
using PetSpaService.VoucherService.VoucherService;
using PRN211GroupProject.Utilities;

namespace PRN211GroupProject.Pages.Admin.VoucherPage
{
    public class EditModel : PageModel
    {
        private readonly IVoucherService _voucherService;

        public EditModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [BindProperty]
        public Voucher Voucher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                var roleClaim = User.FindFirst(ClaimTypes.Role);
                
                if (id == null || _voucherService.GetVoucherList() == null)
                {
                    return NotFound();
                }

                var voucher = _voucherService.GetVoucher((int)id);
                if (voucher == null || voucher.Id <= 0)
                {
                    return NotFound();
                }
                Voucher = voucher;
                return Page();
            }
            catch
            {
                return BadRequest();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (Voucher != null && !String.IsNullOrEmpty(Voucher.Name) && Voucher.Discount <= 100 && Voucher.Discount >= 1 && Voucher.Reach >= 1 && Voucher.Expired.Date > Voucher.Created.Date)
                {
                    Voucher.Name = FormatUtilities.TrimSpacesPreserveSingle(Voucher.Name);
                    _voucherService.UpdateVoucher(Voucher);
                    return RedirectToPage("./Index");
                }
                return BadRequest();

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
