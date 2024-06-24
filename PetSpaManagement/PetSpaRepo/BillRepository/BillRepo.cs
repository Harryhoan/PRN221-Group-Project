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

        public Bill GetBill(int billId) => BillDAO.Instance.GetBill(billId);
    }
}
