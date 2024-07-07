using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.VoucherRepository
{
    public class VoucherRepo : IVoucherRepo
    {
        public List<Voucher> GetVoucherList() => VoucherDAO.Instance.GetAllVoucher();
    }
}
