using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.VoucherRepository
{
    public interface IVoucherRepo
    {
        public List<Voucher> GetVoucherList();
    }
}
