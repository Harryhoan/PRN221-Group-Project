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

        public List<Voucher> GetVoucherList()=> voucherRepo.GetVoucherList();
        public Voucher GetVoucher(int id) => voucherRepo.GetVoucher(id);

        public void AddVoucher(Voucher voucher)
        {
            if (voucher == null || voucher.Id != default)
                throw new Exception("Invalid voucher cannot be added");
            voucherRepo.AddVoucher(voucher);
        }

        public void UpdateVoucher(Voucher voucher)
        {
            if (voucher == null)
                throw new Exception("Invalid voucher");
            voucherRepo.UpdateVoucher(voucher);
        }

        public void DeleteVoucher(int voucherID)
        {
            if (!(voucherID > 0))
                throw new Exception("Invalid voucher ID");
            voucherRepo.DeleteVoucher(voucherID);
        }
    }
}
