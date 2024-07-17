﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.VoucherService.VoucherService;
using PRN211GroupProject.Utilities;

namespace PRN211GroupProject.Pages.Admin.VoucherPage
{
    public class CreateModel : PageModel
    {
        private readonly IVoucherService _voucherService;

        public CreateModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
            Voucher = new();
        }

        public IActionResult OnGet()
        {
            try
            {
                return Page();
            }
            catch
            {
                return BadRequest();
            }
        }

        [BindProperty]
        public Voucher Voucher { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (_voucherService == null)
                {
                    return BadRequest();
                }
                if (Voucher == null || Voucher.Id <= 0)
                {
                    return Page();
                }
                if (String.IsNullOrEmpty(Voucher.Name))
                {
                    return BadRequest();
                }
                if (Voucher.Expired.Date <= DateTime.Today.Date)
                {
                    return BadRequest();
                }
                if (Voucher.Discount < 1)
                {
                    return BadRequest();
                }
                Voucher.Name = FormatUtilities.TrimSpacesPreserveSingle(Voucher.Name);
                Voucher.Status = false;
                _voucherService.AddVoucher(Voucher);
                return RedirectToPage();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
