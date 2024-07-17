using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.BillRepository
{
    public interface IBillRepo
    {
        public void AddBill(Bill bill);

        public void DeleteBill(int billId);

        public List<Bill> GetBillList();
        public List<Bill> GetBillCreatedThisYearList();
        public List<Bill> GetAccountBillList(int accId);

        public Bill GetBill(int billId);
        public List<Bill> GetFilterdAccountBill(DateTime fromDate, DateTime toDate, int id);
        public List<Bill> GetFilteredBill(DateTime fromDate, DateTime toDate);

    }
}
