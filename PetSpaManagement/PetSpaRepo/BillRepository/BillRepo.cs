using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.BillRepository
{
    public class BillRepo : IBillRepo
    {
        public void AddBill(Bill bill) => BillDAO.Instance.AddBill(bill);

        public void DeleteBill(int billId) => BillDAO.Instance.DeleteBill(billId);

        public List<Bill> GetBillList() => BillDAO.Instance.GetAllBill();

		public List<Bill> GetAccountBillList(int accId) => BillDAO.Instance.GetAccountBill(accId);

		public Bill GetBill(int billId) => BillDAO.Instance.GetBill(billId);
        public List<Bill> GetFilterdAccountBill(DateTime fromDate, DateTime toDate, int id) => BillDAO.Instance.GetFilterdAccountBill(fromDate, toDate, id);    
        public List<Bill> GetFilteredBill(DateTime fromDate, DateTime toDate) => BillDAO.Instance.GetFilteredBill(fromDate, toDate);
    }
}
