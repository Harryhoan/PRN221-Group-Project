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
    public class IndexModel : PageModel
    {
        private readonly PetSpaDaos.PetSpaManagementContext _context;

        public IndexModel(PetSpaDaos.PetSpaManagementContext context)
        {
            _context = context;
        }

        public IList<Voucher> Voucher { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Vouchers != null)
            {
                Voucher = await _context.Vouchers.ToListAsync();
            }
        }
    }
}
