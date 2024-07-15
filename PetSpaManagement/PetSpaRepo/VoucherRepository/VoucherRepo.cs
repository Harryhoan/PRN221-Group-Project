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
        public Voucher GetVoucher(int id) => VoucherDAO.Instance.GetVoucher(id);

        public void AddVoucher(Voucher voucher) => VoucherDAO.Instance.AddVoucher(voucher);

        public void UpdateVoucher(Voucher voucher) => VoucherDAO.Instance.UpdateVoucher(voucher);

        public void DeleteVoucher(int voucherID) => VoucherDAO.Instance.DeleteVoucher(voucherID);
    }
}
