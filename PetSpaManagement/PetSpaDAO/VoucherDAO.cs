using PetSpaBussinessObject;
using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class VoucherDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static VoucherDAO instance = null;

        public VoucherDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static VoucherDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VoucherDAO();
                }
                return instance;
            }
        }

        public List<Voucher> GetAllVoucher()
        {
            var vouchers = context.Vouchers.ToList();
            if (vouchers == null)
                throw new Exception("All vouchers cannot be retrieved");
            return vouchers;

        }

		public List<Voucher> GetActiveVoucher()
		{
			var vouchers = context.Vouchers.Where(v => v.Expired > DateTime.Now).ToList();
			if (vouchers == null)
				throw new Exception("All vouchers cannot be retrieved");
			return vouchers;

		}
		public Voucher GetVoucher(int id)
        {
            return context.Vouchers.SingleOrDefault(v => v.Id.Equals(id));
        }
        public void AddVoucher(Voucher voucher)
        {
            Voucher newVoucher = GetVoucher(voucher.Id);
            if(newVoucher == null)
            {
                context.Vouchers.Add(voucher);
                context.SaveChanges();
            }
        }
        public void UpdateVoucher(Voucher voucher)
        {
            if(voucher == null)
            {
                throw new ArgumentNullException(nameof(voucher), "Voucher cannot be null");
            }
            var existingVoucher = context.Vouchers.FirstOrDefault(s => s.Id == voucher.Id);
            if (existingVoucher == null)
            {
                throw new Exception("Voucher does not exist");
            }
            context.Entry(existingVoucher).CurrentValues.SetValues(voucher);
            context.SaveChanges();
        }
        public void DeleteVoucher(int voucherID)
        {
            Voucher voucher = GetVoucher(voucherID);
            if(voucher != null)
            {
                voucher.Status = false;
                context.SaveChanges();
            }
        }
    }
}
