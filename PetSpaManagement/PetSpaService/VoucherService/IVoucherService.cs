using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.VoucherService.VoucherService
{
    public interface IVoucherService
    {
        public List<Voucher> GetVoucherList();
        public Voucher GetVoucher(int id);
        public void AddVoucher(Voucher voucher);
        public void UpdateVoucher(Voucher voucher);
        public void DeleteVoucher(int voucherID);

		public List<Voucher> GetActiveVoucherList();
	}
}
