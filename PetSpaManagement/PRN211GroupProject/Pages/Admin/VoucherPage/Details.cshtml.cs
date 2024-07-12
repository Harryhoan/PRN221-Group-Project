using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;

namespace PRN211GroupProject.Pages.Admin.VoucherPage
{
    public class DetailsModel : PageModel
    {
        private readonly PetSpaDaos.PetSpaManagementContext _context;

        public DetailsModel(PetSpaDaos.PetSpaManagementContext context)
        {
            _context = context;
        }

      public Voucher Voucher { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Vouchers == null)
            {
                return NotFound();
            }

            var voucher = await _context.Vouchers.FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
