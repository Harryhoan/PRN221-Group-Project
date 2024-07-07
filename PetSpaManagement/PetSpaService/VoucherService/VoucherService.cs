using PetSpaBussinessObject;
using PetSpaRepo.VoucherRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.VoucherService.VoucherService
{
    public class VoucherService : IVoucherService
    {
        private IVoucherRepo voucherRepo;

        public VoucherService()
        {
            voucherRepo = new VoucherRepo();
        }

        public List<Voucher> GetVoucherList()
        {
            return voucherRepo.GetVoucherList();
        }
    }
}
