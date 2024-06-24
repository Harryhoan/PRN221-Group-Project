using PetSpaBussinessObject;
using PetSpaRepo.BillRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.BillService
{
    public class BillService : IBillService
    {
        private IBillRepo billRepo;
        public BillService()
        {
            billRepo = new BillRepo();
        }

        public Bill GetBill(int billId)
        {
            return billRepo.GetBill(billId);
        }

        public List<Bill> GetBillList()
        {
            return billRepo.GetBillList();
        }

		public List<Bill> GetAccountBillList(int accId)
		{
			return billRepo.GetAccountBillList(accId);
		}


		public void AddBill(Bill bill)
        {
            if (bill == null || bill.Id != default)
                throw new Exception("Invalid bill cannot be added");
            if (bill.Total <= 0)
                throw new Exception("Bill has invalid price");
            billRepo.AddBill(bill);
        }

        public void DeleteBill(int billId)
        {
            if (!(billId > 0))
                throw new Exception("Invalid bill id");
            billRepo.DeleteBill(billId);
        }

    }
}
